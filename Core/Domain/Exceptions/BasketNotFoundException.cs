using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class BasketNotFoundException(string Id):NotFoundException($"Basket With this{Id} is Not Found")
    {
    }
}
