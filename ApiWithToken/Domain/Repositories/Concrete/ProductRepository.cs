using ApiWithToken.Domain.Context;
using ApiWithToken.Domain.Model;
using ApiWithToken.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Repositories.Concrete
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(ApiWihTokenDbContext context) : base(context)
        {
        }

        public async Task AddProductAsync(Products product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task<Products> FindProductAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<IEnumerable<Products>> ListAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task RemoveProductAsync(int productId)
        {
            var product = await FindProductAsync(productId);

            _context.Products.Remove(product);
        }

        public async Task UpdateProductAsync(Products products)
        {
            _context.Products.Update(products);

        }
    }
}
