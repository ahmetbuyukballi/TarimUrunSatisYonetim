using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class PictureProducts : EntityBase
    {
        public int ProductId { get; set; }
        public string PictureWay { get; set; }
        public Products Products { get; set; }
    }
}
