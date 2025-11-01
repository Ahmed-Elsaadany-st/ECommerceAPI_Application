using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModule
{
    public class OrderItem : BaseEntity<int>
    {
        public ProductItemOrdered product { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
