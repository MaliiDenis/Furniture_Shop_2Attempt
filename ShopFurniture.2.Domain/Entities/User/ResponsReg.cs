using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFurniture._2.Domain.Entities.User
{
    public class ResponsReg //Возвращение ответа на View 
    {

        public bool Succece { get; set; }
        public string RespMsg { get; set; } //вывод сообщения об ошибке
        public RegisterE User { get; set; }
    }
}
