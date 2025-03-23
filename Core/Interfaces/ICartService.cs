using System;
using Core.Entities;

namespace Core.Interfaces;

public interface ICartService
{
  Task<ShoppingCart?> GetCartAsync(int Id);
   Task<ShoppingCart?> SetCartAsync(ShoppingCart cart);
   Task<bool> DeleteCartAsync(int Id);
   
}
