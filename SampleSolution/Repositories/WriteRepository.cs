using System;
using System.Collections.Generic;
using SampleSolution.Common.UnitOfWork;
using SampleSolution.Data.Contexts;

namespace SampleSolution.Repositories
{
    public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : class
    {
        private readonly SomeDataContext _dataContext;

        public WriteRepository(SomeDataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public TEntity Get(Guid id)
        {
            return _dataContext.Set<TEntity>().Find(id);
        }

        public void Add(TEntity entity)
        {
            _dataContext.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dataContext.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _dataContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dataContext.Set<TEntity>().RemoveRange(entities);
        }
    }
}
