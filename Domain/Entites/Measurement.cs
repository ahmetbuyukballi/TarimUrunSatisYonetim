using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Measurement:EntityBase
    {
        public double Kg { get; set; }
        public double Ml { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
