using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SampleSolution.Common.UnitOfWork
{
    public interface IReadRepository<TEntity> where TEntity : class
    {
        TEntity Get(Guid id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicates);
    }
}
