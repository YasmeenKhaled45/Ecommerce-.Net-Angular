using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
  Task<T?> GetById(int Id);
  Task<IReadOnlyList<T>> GetAllAsync();
  Task<T?> GetEntitywithSpec(ISpecification<T> spec);
  Task<IReadOnlyList<T>> ListAllAsync(ISpecification<T> spec);
   Task<TResult?> GetEntitywithSpec<TResult>(ISpecification<T,TResult> spec);
  Task<IReadOnlyList<TResult>> ListAllAsync<TResult>(ISpecification<T,TResult> spec);
  void Add(T entity);
  void Update(T entity);
  void Remove(T entity);
  Task<bool> SaveChangesAsync();
  Task<int> CountAsync(ISpecification<T> spec);
  bool Exists(int Id);
  
}
