using Domain.Common;
using Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Products:EntityBase
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public Details Details { get; set; }  // Navigation property to Details

        public Guid MeasurementId { get; set; }
        public Measurement Measurement { get; set; }

        public int Stock { get; set; }

        [Required]
        public Guid CategoriesId { get; set; }
        public Categories Categories { get; set; }

        [Required]
        public Guid BrandId { get; set; }
        public Brands Brand { get; set; }

        public ICollection<Comments> Comments { get; set; }
        public ICollection<Orders> Orders { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }
    }

}
