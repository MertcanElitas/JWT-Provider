using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiWithToken.Domain.Model;
using ApiWithToken.Domain.Services;
using ApiWithToken.Extensions;
using ApiWithToken.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiWithToken.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var productListResponse = await _productService.ListAsync();

            if (productListResponse.IsSuccess)
                return Ok(productListResponse.Entity);
            else
                return BadRequest(productListResponse.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetFindById(int id)
        {
            var productResponse = await _productService.FindByIdAsync(id);

            if (productResponse.IsSuccess)
                return Ok(productResponse.Entity);
            else
                return BadRequest(productResponse.Message);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductResource productResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessage());
            }
            else
            {
                var product = _mapper.Map<ProductResource, Products>(productResource);

                var productResponse = await _productService.AddProduct(product);

                if (productResponse.IsSuccess)
                    return Ok(productResponse.Entity);
                else
                    return BadRequest(productResponse.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(ProductResource productResource, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessage());
            }
            else
            {
                var product = _mapper.Map<ProductResource, Products>(productResource);

                var processResult = await _productService.UpdateProduct(product, id);

                if (processResult.IsSuccess)
                    return Ok(processResult.Entity);
                else
                    return BadRequest(processResult.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var response = await _productService.RemoveProduct(id);

            if (response.IsSuccess)
            {
                return Ok(response.Entity);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
    }
}