using System;
using System.Text.Json;
using Core.Entities;

namespace Infrustructure.Data.SeedData;

public class StoreContextSeed
{
  public static async Task SeedAsync(StoreContext context)
  {
     if(!context.Products.Any())
     {
        var productsData = await File.ReadAllTextAsync("../Infrustructure/Data/SeedData/products.json");
        var products = JsonSerializer.Deserialize<List<Product>>(productsData);

        if(products is null) return ;
        context.Products.AddRange(products);
        await context.SaveChangesAsync();
        
     }
  }
}
