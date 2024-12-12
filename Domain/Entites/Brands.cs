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
    public class Brands:EntityBase
    {
        public string Name { get; set; }
        public ICollection<Products> products { get; set; }
        [Required]
        [ForeignKey("CategoriesId")]
        public int CategoriesId { get; set; }
        public ICollection<Categories> categories { get; set; }
    }
}
