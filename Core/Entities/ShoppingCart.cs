using System;

namespace Core.Entities;

public class ShoppingCart : BaseEntity
{
   public List<Items> items { get; set; } = new List<Items>(); 
}
