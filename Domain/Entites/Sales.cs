using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Sales : EntityBase
    {
        public string Name { get; set; }
        public double Rate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid ProductsId { get; set; }
        public ICollection<Products> Products { get; set; }
        public Guid CategoriesId { get; set; }
        public ICollection<Categories> Categories { get; set; }
        public Guid BrandsId { get; set; }
        public ICollection<Brands> Brands { get; set; }


    }
}
