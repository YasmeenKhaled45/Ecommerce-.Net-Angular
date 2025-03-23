using System;
using Core.Entities;
using Core.Interfaces;

namespace Infrustructure.Data;

public class SpecificationEvaluator<T> where T : BaseEntity
{

  public static IQueryable<T> GetQuery(IQueryable<T> query , ISpecification<T> spec)
  {
    if(spec.Criteria != null)
    {
        query = query.Where(spec.Criteria);
    }
    if(spec.OrderBy!= null)
    {
        query = query.OrderBy(spec.OrderBy);
    }
    if(spec.OrderByDescending != null)
    {
        query = query.OrderByDescending(spec.OrderByDescending);
    }
    if(spec.IsDistinct ){
        query = query.Distinct();
    }
    if(spec.IsPaginated){
      query = query.Skip(spec.Skip).Take(spec.Take);
    }
    return query;
  }
  public static IQueryable<TResult> GetQuery<TSpec,TResult>(IQueryable<T> query ,
   ISpecification<T,TResult> spec)
  {
    if(spec.Criteria != null)
    {
        query = query.Where(spec.Criteria);
    }
    if(spec.OrderBy!= null)
    {
        query = query.OrderBy(spec.OrderBy);
    }
    if(spec.OrderByDescending != null)
    {
        query = query.OrderByDescending(spec.OrderByDescending);
    }
    var selectquery = query as IQueryable<TResult>;
    if(spec.Select!= null){
        selectquery = query.Select(spec.Select);
    }
    if(spec.IsDistinct)
    {
        selectquery = selectquery?.Distinct();
    }
    if(spec.IsPaginated){
        selectquery = selectquery?.Skip(spec.Skip).Take(spec.Take);
    }
    
    return selectquery ?? query.Cast<TResult>();
  }
}
