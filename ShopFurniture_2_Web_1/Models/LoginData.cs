using Newtonsoft.Json;
using ShopFurniture._2.Domain.Entities.User.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopFurniture_2_Web.Models
{
    public class LoginData 
    {
        public int ID { get; set; }
        public string Username { get; set;}
        public string Password { get; set;}
        //public Users name { get; internal set; }
        public string Name { get; internal set; }
    }
}