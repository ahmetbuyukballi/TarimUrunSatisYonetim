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
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid CartId { get; set; }
        public CardInformation CardInformation { get; set; }
        public double Amound { get; set; }

    }
}
