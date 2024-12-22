using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CreateProductsdTOS
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int MeasurementId { get; set; }
        public int Stock { get; set; }
        public int CategoriesId { get; set; }
        public int BrandId { get; set; }
    }
}
