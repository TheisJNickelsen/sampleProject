using System;
using Microsoft.Extensions.Configuration;
using SampleSolution.Data.Contexts;
using SampleSolution.Data.Contexts.Factories;

namespace SampleSolution.Repositories.UnitOfWork
{
    public class EntityFrameworkUnitOfWork : IEntityFrameworkUnitOfWork
    {
        private readonly SomeDataContext _dbContext;
        private IBusinessUserRepository _businessUserRepository;
        private ISomeDataReadRepository _someDataReadRepository;
        private ISomeDataWriteRepository _someDataWriteRepository;
        private bool _disposed;

        public EntityFrameworkUnitOfWork(IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            _dbContext = EntityFrameworkContextFactory.CreateSomeDataContext(configuration);
        }

        public IBusinessUserRepository BusinessUserRepository
        {
            get
            {
                return _businessUserRepository = _businessUserRepository ?? new BusinessUserRepository(_dbContext);
            }
        }

        public ISomeDataReadRepository SomeDataReadRepository
        {
            get
            {
                return _someDataReadRepository = _someDataReadRepository ?? new SomeDataReadRepository(_dbContext);
            }
        }

        public ISomeDataWriteRepository SomeDataWriteRepository
        {
            get
            {
                return _someDataWriteRepository = _someDataWriteRepository ?? new SomeDataWriteRepository(_dbContext);
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
