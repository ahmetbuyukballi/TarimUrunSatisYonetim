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
        public int ProductsId { get; set; }
        public ICollection<Products> Products { get; set; }
        public int CategoriesId { get; set; }
        public ICollection<Categories> Categories { get; set; }
        public int BrandsId { get; set; }
        public ICollection<Brands> Brands { get; set; }


    }
}
