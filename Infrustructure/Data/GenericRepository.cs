using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.Data;

public class GenericRepository<T>(StoreContext context) : IGenericRepository<T> where T : BaseEntity
{
    private readonly StoreContext context1 = context;

    public void Add(T entity)
    {
       context1.Set<T>().Add(entity);
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        var query = context1.Set<T>().AsQueryable();
        query = spec.ApplyCriteria(query);
      return await query.CountAsync();
    }

    public bool Exists(int Id)
    {
       return context1.Set<T>().Any(x=>x.Id == Id);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await context1.Set<T>().ToListAsync();
    }

    public async Task<T?> GetById(int Id)
    {
        return await context1.Set<T>().FindAsync(Id);
    }

    public async Task<T?> GetEntitywithSpec(ISpecification<T> spec)
    {
        return await Applyspecification(spec).FirstOrDefaultAsync();
        
    }

    public async Task<TResult?> GetEntitywithSpec<TResult>(ISpecification<T, TResult> spec)
    {
        return await Applyspecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAllAsync(ISpecification<T> spec)
    {
        return await Applyspecification(spec).ToListAsync();
    }

    public async Task<IReadOnlyList<TResult>> ListAllAsync<TResult>(ISpecification<T, TResult> spec)
    {
        return await Applyspecification(spec).ToListAsync();

    }

    public void Remove(T entity)
    {
       context1.Set<T>().Remove(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context1.SaveChangesAsync() > 0;
    }

    public void Update(T entity)
    {
       context1.Set<T>().Attach(entity);
       context1.Entry(entity).State = EntityState.Modified;
    }
    private IQueryable<T> Applyspecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(context1.Set<T>().AsQueryable(),spec);
    }
    private IQueryable<TResult> Applyspecification<TResult>(ISpecification<T,TResult> spec)
    {
          return SpecificationEvaluator<T>.GetQuery<T,TResult>(context1.Set<T>().AsQueryable(),spec);
    }
}
