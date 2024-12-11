using Domain.Common;
using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Transaction : EntityBase
    {
        public int UserId { get; set; }
        public AppUser AppUser { get; set; }
        public int CartId { get; set; }
        public CardInformation CardInformation { get; set; }
        public double Amound { get; set; }

    }
}
