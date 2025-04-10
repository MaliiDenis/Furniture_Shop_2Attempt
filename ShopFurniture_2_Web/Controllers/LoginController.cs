using ShopFurniture._2.BusinessLogic.Interfaces;
using ShopFurniture._2.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopFurniture_2_Web.Models;
using ShopFurniture._2.Domain;
using ShopFurniture._2.Domain.Entities.GeneralResponce;
using ShopFurniture._2.Domain.Entities.User;
using ShopFurniture._2.Domain.Entities.Auth;

namespace ShopFurniture_2_Web.Controllers
{
    public class LoginController : Controller
    {
        internal ISession _session;

        public LoginController()
        {
            var logicBL = new ShopFurniture._2.BusinessLogic.BussinesLogic();
            _session = logicBL.GetSessionBL();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registration() // вызываем страницу форму (регистрации)
        {
            return View();
        }

        [HttpPost] 
        public ActionResult RegistPost(RegistModel newUser) //делаем логику для обработки регистрации, newUser = любое имя
        {
            var user = new RegisterE // все поля что и в моделе создаем в Domain такую же модель и заполняем поля ниже
            {
                UserName = newUser.UserName,
                Email = newUser.Email,
                Password = newUser.Password,
                DataRegist = DateTime.Now,
                Ip = "1.1.1.0",
                // можно и доп поля
             

            };
            //делаем в Domain возврат и в Issesion (интерфейс) то что будет возвращаться
            var resp = _session.SerNewUser(user);

            if (resp.Succece)
            {
                return RedirectToAction("Index", "Home");
            }else
            {
                return View("Registration", resp);
            }
        }

        [HttpPost]
        public ActionResult LoginPost(LoginData data)
        {
            var UserLoginData = new ULoginData
            {
                Credential = data.Username,
                Password = data.Password,
                LoginIp = HttpContext.Request.UserHostAddress,
                LoginDateTime = DateTime.Now,
            };
            RResponceData response = _session.UserLoginAction(UserLoginData);

            if (response != null && response.Status)
            {
                HttpCookie cookie = _session.GenCoockieAlgo(data.Name);
                ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                return RedirectToAction("Index", "Home");
            }
            return View("Login", data);
        }
    }
}
