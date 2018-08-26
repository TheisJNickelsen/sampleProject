using System;

namespace SampleSolution.Repositories.UnitOfWork
{
    public interface IEntityFrameworkUnitOfWork : IDisposable
    {
        IBusinessUserRepository BusinessUserRepository { get; }
        ISomeDataReadRepository SomeDataReadRepository { get; }
        ISomeDataWriteRepository SomeDataWriteRepository { get; }
        void Save();
    }
}
