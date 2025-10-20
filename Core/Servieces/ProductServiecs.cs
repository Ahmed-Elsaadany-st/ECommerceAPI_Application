using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Servieces.Abstractions;
using Servieces.Specifications;
using Shared;
using Shared.DTOs;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servieces
{
    public class ProductServiecs(IUntiOfWork _untiOfWork,IMapper _mapper) : IProductServices
    {
        #region Detailed Steps
        //public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        //{
        //    var repo = _untiOfWork.CreateRepository<ProductBrand, int>(); //Create Repository
        //    var brands = await repo.GetAllAsync(); // Get Data
        //    return _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(brands); // Mapping and Returnig the data

        //} 
        #endregion
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()=>
              _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(await _untiOfWork.GetRepository<ProductBrand, int>().GetAllAsync());
           
        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProcductQueryParams queryParams)
        {
            var Repo=_untiOfWork.GetRepository<Product,int>();
            var AllProducts=await Repo.GetAllAsync(new ProductWithBrandAndTypeSpecifications(queryParams));
            var ProductCount = AllProducts.Count();// Get All Products in that page(one page)
            var Data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(AllProducts);
            var CountSpec = new ProductCountSpecifications(queryParams);
            var TotalCount = await Repo.CountAsync(CountSpec);
            return new PaginatedResult<ProductDto>(queryParams.PageSize,queryParams.PageIndex, TotalCount, Data);

        }
        //Before Paginated Result I used this line==>
       //=>_mapper.Map<IEnumerable<Product>,IEnumerable<ProductDto>>(await _untiOfWork.GetRepository<Product,int>().GetAllAsync(new ProductWithBrandAndTypeSpecifications(queryParams)));

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        =>_mapper.Map<IEnumerable<ProductType>,IEnumerable<TypeDto>>(await _untiOfWork.GetRepository<ProductType, int>().GetAllAsync());

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var Specifications = new ProductWithBrandAndTypeSpecifications(id);
            var Product = await _untiOfWork.GetRepository<Product, int>().GetByIdAsync(Specifications);
            if (Product is null)
            {
                throw new ProductNotFoundException(id);
            }
            return _mapper.Map<Product, ProductDto>(Product);

        }

       //=>_mapper.Map<Product,ProductDto>(await _untiOfWork.GetRepository<Product,int>().GetByIdAsync(new ProductWithBrandAndTypeSpecifications(id)));
    }
}
