using System;
using Core.DTOS;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
   Task<IReadOnlyList<Product>> GetProducts(string? type , string? brand, string? sort);
   Task<IReadOnlyList<string>> GetBrandsAsync();
   Task<IReadOnlyList<string>> GetTypesAsync();
   Task<Product?> GetProductById(int Id);
   void AddProduct(Product product);
   void UpdateProduct(Product product );
   void DeleteProduct(Product product);
   bool ProductExists(int Id);
   Task<bool> SaveChangesAsync();

}
