using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
//модель для формы 
namespace ShopFurniture_2_Web.Models
{
    public class RegistModel // поля для формы => Контроллер
    { 
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }


        //и другие поля с формы
    }
}