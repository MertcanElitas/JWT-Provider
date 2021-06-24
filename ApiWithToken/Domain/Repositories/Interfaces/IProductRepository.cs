using ApiWithToken.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> ListAsync();

        Task AddProductAsync(Products product);

        Task RemoveProductAsync(int productId);

        Task UpdateProductAsync(Products products);

        Task<Products> FindProductAsync(int productId);

    }
}
