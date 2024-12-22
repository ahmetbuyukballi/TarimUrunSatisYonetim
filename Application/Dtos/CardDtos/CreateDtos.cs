using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.CardDtos
{
    public class CreateDtos
    {
        public string CardHolderName { get; set; }

        [Required]
        [MaxLength(16)]
        public string CardNumber { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        [MaxLength(3)]
        public string CVV { get; set; } 
    }
}
