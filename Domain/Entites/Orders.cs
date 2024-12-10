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
    public class Orders:EntityBase
    {
        public string OrderStatus { get; set; }
        public string Product { get; set; }

        [Required]
        [ForeignKey("ProductId")]

        public Guid ProductId { get; set; }
        public ICollection<Products> products { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
