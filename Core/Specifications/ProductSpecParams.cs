using System;
using System.Security.Cryptography;

namespace Core.Specifications;

public class ProductSpecParams
{
   private const int MaxPageSize = 50;
   public int PageIndex{get;set;} = 1;
   private int _pagesize = 10;
  public int PageSize
  {
    get => _pagesize;
    set => _pagesize = (value > MaxPageSize) ? MaxPageSize : (value <= 0 ? 10 : value); 
  }
 private List<string> brands = [];
 public List<string> Brands
 {
    get => brands;
    set {
       brands = value.SelectMany(x=>x.Split(',',StringSplitOptions.RemoveEmptyEntries)).ToList();
     }
 }
 private List<string> types = [];
 public List<string> Types
 {
    get => types;
    set {
       types = value.SelectMany(x=>x.Split(',',StringSplitOptions.RemoveEmptyEntries)).ToList();
     }
 }
 public string? sort{get;set;}
 private string? _search{get;set;}
 public string? Search
 {
   get=> _search ?? "";
   set=> _search = value?.ToLower();
 }

}
