using System;
using Core.Entities;

namespace Core.Specifications;

public class BrandListSpec : Specification<Product,string>
{
  public BrandListSpec()
  {
    AddSelect(x=>x.Brand);
    ApplyDistinct();
  }
}
