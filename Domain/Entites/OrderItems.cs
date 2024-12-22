using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class OrderItems:EntityBase
    {
        
            public int Id { get; set; }

            // İlişkili Order
            public int OrderId { get; set; }
            public Orders Order { get; set; }

            // İlişkili Product
            public int ProductId { get; set; }
            public Products Product { get; set; }

            // Sipariş edilen ürünün detayları
            public string ProductName { get; set; }
            public double Price { get; set; }
            public string Brand { get; set; }

            // Sipariş edilen miktar
            public int Quantity { get; set; }
        
    }
}
