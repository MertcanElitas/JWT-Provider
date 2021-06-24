using ApiWithToken.Domain.Model;
using ApiWithToken.Domain.Repositories.Interfaces;
using ApiWithToken.Domain.Repositories.UnitOfWork;
using ApiWithToken.Domain.Services;
using ApiWithToken.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ProductResponse> AddProduct(Products product)
        {
            try
            {
                await _productRepository.AddProductAsync(product);
                await _unitOfWork.CompleteAsnyc();

                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"Ürün eklenirken hata oluştu {ex.Message}");
            }
        }

        public async Task<ProductResponse> FindByIdAsync(int productId)
        {
            try
            {
                var product = await _productRepository.FindProductAsync(productId);

                if (product == null)
                {
                    return new ProductResponse("Ürün Bulunamadı");
                }

                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"Ürün bulunurken hata oluştu {ex.Message}");
            }
        }

        public async Task<ProductListResponse> ListAsync()
        {
            try
            {
                var products = await _productRepository.ListAsync();
                return new ProductListResponse(products);
            }
            catch (Exception ex)
            {
                return new ProductListResponse($"Ürün listelenirken hata oluştu {ex.Message}");
            }
        }

        public async Task<ProductResponse> RemoveProduct(int id)
        {
            try
            {
                var product = await _productRepository.FindProductAsync(id);

                if (product == null)
                {
                    return new ProductResponse("Silmeye Çalıştığınız Ürün Bulunamadı");
                }
                else
                {
                    await _productRepository.RemoveProductAsync(id);

                    await _unitOfWork.CompleteAsnyc();

                    return new ProductResponse(product);
                }
            }
            catch (Exception ex)
            {
                return new ProductResponse($"Ürün listelenirken hata oluştu {ex.Message}");
            }
        }

        public async Task<ProductResponse> UpdateProduct(Products product, int productId)
        {
            try
            {
                var model = await _productRepository.FindProductAsync(productId);

                if (model == null)
                {
                    return new ProductResponse("Güncellemeye Çalıştığınız Ürün Bulunamadı");
                }
                else
                {
                    model.Name = product.Name;
                    model.Category = product.Category;
                    model.Price = product.Price;

                    await _unitOfWork.CompleteAsnyc();

                    return new ProductResponse(product);
                }
            }
            catch (Exception ex)
            {
                return new ProductResponse("Ürün Güncellenirken Hata Oluşut" + ex.Message);
            }
        }
    }
}
