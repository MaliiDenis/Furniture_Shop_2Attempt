using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopFurniture._2.Domain.Entities.User;

namespace ShopFurniture._2.BusinessLogic.DBModel
{
    public class SessionContext : DbContext
    {
        public SessionContext() : base("name=ShopFurniture2")
        {
        }

        public virtual DbSet<Session> Sessions { get; set; }
    }
}
