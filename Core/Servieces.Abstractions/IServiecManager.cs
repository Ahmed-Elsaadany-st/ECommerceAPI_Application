using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servieces.Abstractions
{
    public interface IServiecManager
    {
        IProductServices ProductServices { get; }
        IBasketService basketService { get; }
    }
}
