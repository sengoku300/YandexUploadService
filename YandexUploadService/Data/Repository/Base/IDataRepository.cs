using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace YandexUploadService.Data.Repository.Base
{
    public interface IDataRepository<TEntity>
    where TEntity : class
    {
        IEnumerable<TEntity> All();
        TEntity Get(Func<TEntity, bool> expression);
        TEntity Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entity);
        IEnumerable<TEntity> Find(Func<TEntity, bool> predicator);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        TEntity Update(TEntity entity);
        void Delete(Func<TEntity, bool> predicator);
        DbSet<TEntity> Collection { get; }
    }
}
