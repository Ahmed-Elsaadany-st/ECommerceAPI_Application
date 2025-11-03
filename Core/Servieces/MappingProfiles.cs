using AutoMapper;
using Domain.Models;
using Domain.Models.BasketModule;
using Domain.Models.IdentityModule;
using Domain.Models.OrderModule;
using Shared.DTOs;
using Shared.DTOs.BasketDtos;
using Shared.DTOs.IdentityDtos;
using Shared.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servieces
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            #region Prodcut
            CreateMap<Product, ProductDto>()
        .ForMember(dist => dist.BrandName, options => options.MapFrom(src => src.ProductBrand.Name))
        .ForMember(dist => dist.TypeName, options => options.MapFrom(src => src.ProductType.Name));

            CreateMap<ProductType, TypeDto>();
            CreateMap<ProductBrand, BrandDto>();

            #endregion
            #region Basket
            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            #endregion
            #region Identity
            CreateMap<AddressDto, Address>().ReverseMap();
            #endregion
            #region Order
            CreateMap<ShippingAddressDto,ShippingAddress>().ReverseMap();
            //-----------------------------------------
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(D => D.DeliveryMethod, o => o.MapFrom(o => o.DeliveryMethod.ShortName))
                .ForMember(d => d.Total, o => o.MapFrom(s => s.GetTotal()));
            //-----------------------------------------------
            CreateMap<OrderItem, OrderItemsDto>()
            .ForMember(d => d.ProductId, o => o.MapFrom(s => s.product.ProductId))
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.product.ProductName))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>())
            .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
            .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Quantity));
            //------------------------
            CreateMap<DeliveryMethod, DeliveryMethodDto>();
            #endregion
        }
    }
}
