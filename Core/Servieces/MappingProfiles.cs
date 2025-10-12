using AutoMapper;
using Domain.Models;
using Shared.DTOs;
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
            CreateMap<Product, ProductDto>()
                .ForMember(dist=>dist.BrandName,options=>options.MapFrom(src=>src.ProductBrand.Name))
                .ForMember(dist=>dist.TypeName,options=>options.MapFrom(src=>src.ProductType.Name));

            CreateMap<ProductType, TypeDto>();
            CreateMap<ProductBrand,BrandDto>();

        }
    }
}
