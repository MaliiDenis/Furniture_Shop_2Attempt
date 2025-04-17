using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopFurniture._2.BusinessLogic.Interfaces;
using ShopFurniture._2.BusinessLogic.Core;
using ShopFurniture._2.Domain.Entities.GeneralResponce;
using ShopFurniture._2.Domain.Entities.User;
using ShopFurniture._2.Domain.Entities.Auth;
using ShopFurniture._2.Domain.Entities.User.DbModel;
using System.Web;
using System.Web.UI.WebControls;

namespace ShopFurniture._2.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public new RResponceData UserLoginAction(ULoginData data)
        {
            return UserLoginActionDb(data);
        }

        //public UCoockieData GenCoockieAlgo(Users dataUser)
        //{
        //    return new UCoockieData(); // Создание нового объекта класса UCoockieData
        //}
        public HttpCookie GenCoockieAlgo(string login)
        {
            return Cookie(login);
        }

        public ResponsReg SerNewUser(RegisterE user)
        {
            return SerNewUserDb(user);
        }
    }
}
