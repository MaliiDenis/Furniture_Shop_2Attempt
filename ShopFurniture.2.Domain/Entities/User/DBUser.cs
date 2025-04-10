using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopFurniture._2.Domain.Enums;

namespace ShopFurniture._2.Domain.Entities.User
{
    public class DBUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public string Loginip { get; set; }
        public Roles Level { get; set; }
    }
}
