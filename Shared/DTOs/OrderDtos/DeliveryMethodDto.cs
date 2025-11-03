using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.OrderDtos
{
    public class DeliveryMethodDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public string DeliveryMethod { get; set; }=null!;
        public string ShortName { get; set; } = null!;
        public decimal Price { get; set; }
         
    }
}
