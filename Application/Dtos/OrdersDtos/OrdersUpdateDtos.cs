using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.OrdersDtos
{
    public class OrdersUpdateDtos
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string BrandName { get; set; }
        public string Details { get; set; }

    }
}
