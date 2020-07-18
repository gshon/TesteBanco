using OperacaoBancaria.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OperacaoBancaria.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity<TEntity>
    {
        void Add(TEntity obj);
        TEntity GetById(Guid id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity obj);
        void Remove(Guid id);
        void Remove(string userId);
        TEntity GetEntity(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);
        int SaveChanges();
    }
}
