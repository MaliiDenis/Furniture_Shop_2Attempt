using ShopFurniture._2.BusinessLogic.Core;
using ShopFurniture._2.Domain.Entities.GeneralResponce;
using ShopFurniture._2.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopFurniture._2.Domain.Entities.Auth;
using ShopFurniture._2.Domain.Entities.User.DbModel;
using System.Web;

namespace ShopFurniture._2.BusinessLogic.Interfaces
{
    public interface ISession
    {
        RResponceData UserLoginAction(ULoginData data);
        HttpCookie GenCoockieAlgo(string login);
        ResponsReg SerNewUser(RegisterE user);  
    }
}
