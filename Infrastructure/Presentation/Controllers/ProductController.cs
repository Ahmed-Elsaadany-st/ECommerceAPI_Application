using Microsoft.AspNetCore.Mvc;
using Servieces.Abstractions;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IServiecManager _serviecManager) : ControllerBase
    {
        //Get All Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GelAllProducts()
        {
            var Products= await _serviecManager.ProductServices.GetAllProductsAsync();
            return Ok(Products);

        }  
        //Get Product By Id
        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductDto>> GetById(int Id)
        {
            var Product = await _serviecManager.ProductServices.GetProductByIdAsync(Id);
            return Ok(Product);
        }
        //Get All Types

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            var Types= await _serviecManager.ProductServices.GetAllTypesAsync();
            return Ok(Types);
        }

        //Get All Brands

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var Brands=await _serviecManager.ProductServices.GetAllBrandsAsync();
            return Ok(Brands);
        }


       

    }   
}
