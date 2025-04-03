using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopFurniture._2.Domain.Entities.User;

namespace ShopFurniture._2.BusinessLogic.DBModel
{
    class UserContext : DbContext
    {
        public UserContext() :
            base("name=ShopFurniture2") // connectionstring name define in your web.config
        {
        }

        public virtual DbSet<UDbTable> Users { get; set; } //Связывает с сервером 
        //public virtual DbSet<DBUser> Users { get; set; } //Связывает с сервером 
    }
}
