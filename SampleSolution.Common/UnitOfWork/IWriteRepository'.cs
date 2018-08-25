using System;
using System.Collections.Generic;

namespace SampleSolution.Common.UnitOfWork
{
    public interface IWriteRepository<TEntity> where TEntity : class
    {
        TEntity Get(Guid id);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
