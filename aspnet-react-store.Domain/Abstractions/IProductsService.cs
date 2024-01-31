﻿using aspnet_react_store.Domain.Models;

namespace aspnet_react_store.Domain.Abstractions
{
    public interface IProductsService
    {
        Task<int> CreateProduct(Product product);
        Task<int> DeleteProduct(int id);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Product>> GetProducts(int? startId, int? count, string? searchText);
        Task<int> UpdateProduct(int id, string name, decimal price, string description);
    }
}