using System.Collections.Generic;
using SampleSolution.Domain.Aggregates;
using SampleSolution.Repositories;

namespace SampleSolution.Services
{
    public class SomeDataReadService : ISomeDataReadService
    {
        private readonly ISomeDataWriteRepository _mySampleSolutionRepository;

        public SomeDataReadService(ISomeDataWriteRepository mySampleSolutionRepository)
        {
            _mySampleSolutionRepository = mySampleSolutionRepository;
        }

        public List<SomeAggregate> GetSomeData(string userEmail)
        {
            return _mySampleSolutionRepository.GetSomeData(userEmail);
        }
    }
}
