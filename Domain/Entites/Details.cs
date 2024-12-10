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
    public class Details:EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        public Guid ProductId { get; set; }  // Foreign key property

        // Navigation property
        public Products Product { get; set; }
    }
}
