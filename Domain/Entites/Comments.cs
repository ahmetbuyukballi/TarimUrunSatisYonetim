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
    public class Comments:EntityBase
    {
        public string Description { get; set; }
        public int Scoring { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }
        public Products Products { get; set; }
    }
}
