using System;
using System.Dynamic;
using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications;

public class Specification<T>(Expression<Func<T,bool>>? criteria): ISpecification<T>
{
    protected Specification() : this(null){}
    public Expression<Func<T, bool>>? Criteria => criteria;

    public Expression<Func<T, object>>? OrderBy  {get; private set;}

    public Expression<Func<T, object>>? OrderByDescending {get;private set;}

    public bool IsDistinct {get;private set;}

    public int Skip {get;private set;}

    public int Take {get;private set;}

    public bool IsPaginated {get;private set;}

    protected void AddOrderBy(Expression<Func<T,object>> orderby)
    {
        OrderBy = orderby;
    }
    protected void AddOrderByDesc(Expression<Func<T,object>> orderbydesc)
    {
       OrderByDescending = orderbydesc;
    }
    protected void ApplyDistinct(){
        IsDistinct = true;
    }
    protected void ApplyPagination(int skip , int take)
    {
        Skip = skip;
        Take = take;
        IsPaginated = true;
    }
    public IQueryable<T> ApplyCriteria(IQueryable<T> query)
    {
          if(Criteria != null){
            query = query.Where(Criteria);
          }
          return query;
    }
}
public class Specification<T, TResult>(Expression<Func<T, bool>> criteria) : Specification<T>(criteria),
  ISpecification<T, TResult>
{
    protected Specification() : this(null!){}
    public Expression<Func<T, TResult>>? Select {get;private set;}
    protected void AddSelect(Expression<Func<T,TResult>> selectExpression)
    {
        Select = selectExpression;
    }
}
