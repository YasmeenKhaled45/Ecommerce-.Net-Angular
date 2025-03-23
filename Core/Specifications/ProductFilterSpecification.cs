using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductFilterSpecification : Specification<Product>
{
  public ProductFilterSpecification(ProductSpecParams specParams) : base(x =>
                (string.IsNullOrEmpty(specParams.Search) || 
                 x.Name.ToLower().Contains(specParams.Search)) &&
                (!specParams.Brands.Any() || specParams.Brands.Contains(x.Brand)) &&
                (!specParams.Types.Any() || specParams.Types.Contains(x.Type)))
  {
    ApplyPagination(specParams.PageSize * (specParams.PageIndex - 1),specParams.PageSize);
    switch(specParams.sort){
        case "priceAsc":
         AddOrderBy(x=>x.Price);
         break;
        case "priceDesc":
        AddOrderByDesc(x=>x.Price);
         break;
         case "TopRated":
         AddOrderByDesc(x=>x.Rate);
         break;
         default:
         AddOrderBy(x=>x.Name);
         break;
    }
  }

  
}
