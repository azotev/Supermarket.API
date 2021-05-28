using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Services.Communication;
using Supermarket.API.Persistence.Contexts;

namespace Supermarket.API.Services
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

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _productRepository.ListAsync();
        }

        public async Task<ProductResponse> GetAsync(int id)
        {
            var existingProduct = await _productRepository.FindByIdAsync(id);
            if (existingProduct == null)
                return new ProductResponse($"Product {id} not found.");

            return new ProductResponse(existingProduct);
        }

        public async Task<ProductResponse> SaveAsync(Product product)
        {
            try
            {
                await _productRepository.AddAsync(product);
                await _unitOfWork.CompleteAsync();

                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"Error when saving category: {ex.Message}");
            }
        }

        public async Task<ProductResponse> UpdateAsync(int id, Product product)
        {
            var existingProduct = await _productRepository.FindByIdAsync(id);
            if (existingProduct == null)
                return new ProductResponse($"Product {id} not found.");

            existingProduct.Name = product.Name;
            existingProduct.QuantityInStock = product.QuantityInStock;
            existingProduct.UnitPrice = product.UnitPrice;
            existingProduct.OrderItems = product.OrderItems;

            try
            {
                _productRepository.Update(product);
                await _unitOfWork.CompleteAsync();

                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"Error in product update: {ex.Message}");
            }
        }

        public async Task<ProductResponse> DeleteAsync(int id)
        {
            var existingProduct = await _productRepository.FindByIdAsync(id);
            if (existingProduct == null)
                return new ProductResponse($"Product {id} not found.");

            try
            {
                _productRepository.Remove(existingProduct);
                await _unitOfWork.CompleteAsync();

                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occured when deleting the category: {ex.Message}");
            }
        }



    }
}
