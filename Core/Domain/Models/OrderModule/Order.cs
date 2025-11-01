using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModule
{
    public class Order :BaseEntity<Guid>
    {

        public Order()
        {
            
        }

        public Order(string userEmail, ICollection<OrderItem> items, DeliveryMethod deliveryMethod, ShippingAddress address, decimal subTotal)
        {
            UserEmail = userEmail;
            Items = items;
            DeliveryMethod = deliveryMethod;
            Address = address;
            SubTotal = subTotal;
        }

        public string UserEmail { get; set; } = null!;
        public ICollection<OrderItem> Items { get; set; } = [];
        public DeliveryMethod DeliveryMethod { get; set; } = null!;
        public ShippingAddress Address { get; set; } = null!;
        public decimal SubTotal { get; set; }
        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.Now;
        public OrderStatus OrderStatus { get; set; }
        public int DeliveryMethodId { get; set; } // FK


        public decimal GetTotal()=>SubTotal+DeliveryMethod.Price;


    }
}
