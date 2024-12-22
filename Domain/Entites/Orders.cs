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
       
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();

        [ForeignKey("CargoInformationId")]
        public int CargoInformationId { get; set; }
        public CargoInformation CargoInformation { get; set; }

    }
}
