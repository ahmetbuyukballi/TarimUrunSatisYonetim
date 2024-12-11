using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class CargoInformation:EntityBase
    {
        public string CargoCompany { get; set; }
        public string CargoName { get; set; }
        public string CargoDescription { get; set; }
        public Guid OrdersId { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }
}
