using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Categories:EntityBase
    {
        public int ParentId { get; set; }
        public int ProirtyId { get; set; }
        public string Name { get; set; }
        public ICollection<Products> Products { get; set; }
        [Required]
        [ForeignKey("BrandsId")]
        public int BrandsId { get; set; }
        public ICollection<Brands> Brands { get; set; }

    }
}
