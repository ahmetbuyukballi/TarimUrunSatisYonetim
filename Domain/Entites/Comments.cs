using Domain.Common;
using Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Comments:EntityBase
    {
        public string Description { get; set; }
        public int Scoring { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [JsonIgnore]
        public AppUser AppUser { get; set; }
        [ForeignKey("ProductsId")]
        public int ProductsId { get; set; }
        [JsonIgnore]
        public Products Products { get; set; }
    }
}
