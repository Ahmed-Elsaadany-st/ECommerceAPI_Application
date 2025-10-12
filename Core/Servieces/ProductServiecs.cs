using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Servieces.Abstractions;
using Shared.DTOs;
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
           
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
       =>_mapper.Map<IEnumerable<Product>,IEnumerable<ProductDto>>(await _untiOfWork.GetRepository<Product,int>().GetAllAsync());

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        =>_mapper.Map<IEnumerable<ProductType>,IEnumerable<TypeDto>>(await _untiOfWork.GetRepository<ProductType, int>().GetAllAsync());

        public async Task<ProductDto?> GetProductByIdAsync(int id)
       =>_mapper.Map<Product,ProductDto>(await _untiOfWork.GetRepository<Product,int>().GetByIdAsync(id));
    }
}
