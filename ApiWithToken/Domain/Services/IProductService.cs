using ApiWithToken.Domain.Model;
using ApiWithToken.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Services
{
    public interface IProductService
    {
        Task<ProductListResponse> ListAsync();

        Task<ProductResponse> AddProduct(Products product);

        Task<ProductResponse> RemoveProduct(int id);

        Task<ProductResponse> UpdateProduct(Products product, int productId);

        Task<ProductResponse> FindByIdAsync(int productId);
    }
}
