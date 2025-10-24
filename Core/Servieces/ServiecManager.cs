using AutoMapper;
using Domain.Contracts;
using Servieces.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servieces
{
    public class ServiecManager(IUntiOfWork _unitOfWork,IBasketRepository _basketRepo,IMapper _mapper) : IServiecManager
    {
        private readonly Lazy<IProductServices> _LazyProductService=new Lazy<IProductServices> (()=>new ProductServiecs(_unitOfWork,_mapper));
        public IProductServices ProductServices => _LazyProductService.Value;


        private readonly Lazy<IBasketService> _LazyBasketService = new Lazy<IBasketService>(() => new BasketService(_basketRepo, _mapper));
        public IBasketService basketService => _LazyBasketService.Value;
    }
}
 