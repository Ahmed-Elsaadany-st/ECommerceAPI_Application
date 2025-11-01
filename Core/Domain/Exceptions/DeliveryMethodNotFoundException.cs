using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class DeliveryMethodNotFoundException(int Id): NotFoundException($"This Delivery Mehtod : {Id} is Not Avalaible right now! Please try again later")
    {
    }
}
