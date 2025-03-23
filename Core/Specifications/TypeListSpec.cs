using System;
using Core.Entities;

namespace Core.Specifications;
public class TypeListSpec : Specification<Product,string>
{
  public TypeListSpec()
  {
    AddSelect(x=>x.Type);
    ApplyDistinct();
  }
}
