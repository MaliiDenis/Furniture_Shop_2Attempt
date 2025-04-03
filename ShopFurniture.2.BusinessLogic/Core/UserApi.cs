using System;
using System.Linq;
using ShopFurniture._2.Domain.Entities.Auth;
using ShopFurniture._2.Domain.Entities.User.DbModel;
using ShopFurniture._2.BusinessLogic.DBModel;
using ShopFurniture._2.Domain.Entities.GeneralResponce;
using ShopFurniture._2.Domain.Entities.User;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using ShopFurniture._2.Helpers;
using System.Web;
using AutoMapper;
using ShopFurniture._2.Domain.Enums;

namespace ShopFurniture._2.BusinessLogic.Core
{
    public class UserApi
    {
        internal RResponceData ULASessionChech(ULoginData data)
        {
            //db connection
            //SELECT USER FROM DB>User WHERE Username=data.Credential and Pawword = data.Password

            //if SELECT VALID OR TRUE

            //RETURN STATUS = true
            using (var db = new UserContext())

            return new RResponceData
            {
                Status = false,
                User = new Users
                {
                    UserName = "Canion"
                }
            };

        }
        //internal UCoockieData UserCoockieGenerationAlg(Users user)
        //{
        //    //HERE WILL BE THE LOGIC TO COOCKIE GENERATION PROCESS

        //    return new UCoockieData
        //    { 
        //        MaxAge = 1710894, 
        //        Coockie = "My not Unique id for this session" 
        //    };
        //}
        internal RResponceData UserLoginActionDb(ULoginData data)
        {

            UDbTable result;
            var validate = new EmailAddressAttribute();
            if (validate.IsValid(data.Credential))
            {
                var pass = LoginHelper.HashGen(data.Password);
                using (var db = new UserContext())

                {
                   

                    result = db.Users.FirstOrDefault(u => u.Email == data.Credential && u.Password == pass);
                }

                if (result == null)
                {
                    return new RResponceData { Status = false, Message = "The Username or Password is Incorrect" };
                }

                using (var todo = new UserContext())
                {
                    result.LasIp = data.LoginIp;
                    result.LastLogin = data.LoginDateTime;
                    todo.Entry(result).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new RResponceData { Status = true };
            }
            else
            {
                var pass = LoginHelper.HashGen(data.Password);
                using (var db = new UserContext())
                {
                     
                    result = db.Users.FirstOrDefault(u => u.Username == data.Credential && u.Password == pass);
                }

                if (result == null)
                {
                    return new RResponceData { Status = false, Message = "The Username or Password is Incorrect" };
                }

                using (var todo = new UserContext())
                {
                    result.LasIp = data.LoginIp;
                    result.LastLogin = data.LoginDateTime;
                    todo.Entry(result).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new RResponceData { Status = true };
            }
        }

        internal HttpCookie Cookie(string login)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(login)
            };

            using (var db = new SessionContext())
            {
                Session curent;
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(login))
                {
                    curent = (from e in db.Sessions where e.Email == login select e).FirstOrDefault();
                }
                else
                {
                    curent = (from e in db.Sessions where e.Username == login select e).FirstOrDefault();
                }

                if (curent != null)
                {
                    curent.CookieString = apiCookie.Value;
                    curent.ExpireTime = DateTime.Now.AddMinutes(60);
                    using (var todo = new SessionContext())
                    {
                        todo.Entry(curent).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new Session
                    {
                        Username = login,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(60)
                    });
                    db.SaveChanges();
                }
            }

            return apiCookie;
        }
        internal UserMinimal UserCookie(string cookie)
        {
            Session session;
            UDbTable curentUser;

            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var bd = new UserContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.Username))
                {
                    curentUser = bd.Users.FirstOrDefault(u => u.Email == session.Username);
                    //var email = db.Users.FirstOrDefault(u => u.Email == newUser.Email);
                }
                else
                {
                    curentUser = bd.Users.FirstOrDefault(u => u.Username == session.Username);
                }
            }

            if (curentUser == null) return null;
            Mapper.Initialize(cfg => cfg.CreateMap<DBUser, UserMinimal>());
            var userminimal = Mapper.Map<UserMinimal>(curentUser);

            return userminimal;
        }
        protected ResponsReg SerNewUserDb(RegisterE newUser)
        {
            ResponsReg resp = new ResponsReg(); //Создаем переменную resp в которой будет хранится новый пользователь

            try
            {
                using (var db = new UserContext())
                {
                    //Обращение к Таблице для сервера , 
                    var user = db.Users.FirstOrDefault(u => u.Username == newUser.UserName);
                    var email = db.Users.FirstOrDefault(u => u.Email == newUser.Email);
                    if (user != null)
                    {
                        resp.RespMsg = "Пользователь с таким именем уже сущесвтует!";
                    }

                    else if (email != null)
                    {
                        resp.RespMsg = "Почта уже была зарегестрирована!";
                    }
                    else
                    {//Таблица для сервера
                        var newU = new UDbTable
                        {
                            //Поля с модели в Domain,RegisterE
                            Username = newUser.UserName,
                            Email = newUser.Email,
                            Password = LoginHelper.HashGen(newUser.Password),
                            LasIp = newUser.Ip,
                            LastLogin = DateTime.Now,
                            //Поле для доступа, с модели ролей URole
                            Level = URole.User
                        };
                        //Добавляем нового пользователя
                        db.Users.Add(newU);
                        //Сохраняем изменения для отправки на сервер
                        db.SaveChanges();
                        //Если зарегестрировались выводится сообщение
                        resp.RespMsg = "Succese!";
                        //Булевое значение для таблицы
                        resp.Succece = true;
                        //Сам новый пользователь
                        resp.User = newUser;
                        //Возвращаем все в ResponseReg
                        return resp;
                    }
                    resp.Succece = false;

                    resp.User = null;

                    return resp;

                }
            }
            catch (Exception ex)
            {
                resp.RespMsg = "Error: " + ex; // вывод ошибки 

                resp.Succece = false;

                resp.User = null;

                return resp;

            }



        }
    }
}
