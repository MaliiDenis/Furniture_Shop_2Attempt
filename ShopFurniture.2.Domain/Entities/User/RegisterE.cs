using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFurniture._2.Domain.Entities.User
{
    public class RegisterE// поля что и в web (RegistModel) => LoginController
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime DataRegist { get; set; }

        public string Ip {  get; set; }
        // можно и доп поля

    }
}
