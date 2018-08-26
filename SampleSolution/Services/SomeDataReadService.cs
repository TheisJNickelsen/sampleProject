using System;
using System.Collections.Generic;
using SampleSolution.Domain.Aggregates;
using SampleSolution.Repositories;

namespace SampleSolution.Services
{
    public class SomeDataReadService : ISomeDataReadService
    {
        private readonly ISomeDataWriteRepository _someDataWriteRepository;
        private readonly ISomeDataReadRepository _someDataReadRepository;

        public SomeDataReadService(ISomeDataWriteRepository somedataWriteRepository,
            ISomeDataReadRepository someDataReadRepository)
        {
            _someDataWriteRepository = somedataWriteRepository ?? throw new ArgumentNullException(nameof(somedataWriteRepository));
            _someDataReadRepository = someDataReadRepository ?? throw new ArgumentNullException(nameof(someDataReadRepository));
        }

        public List<SomeAggregate> GetSomeData(string userEmail)
        {
            return _someDataReadRepository.GetSomeData(userEmail);
        }
    }
}
