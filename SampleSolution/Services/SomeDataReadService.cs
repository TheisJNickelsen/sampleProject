using System.Collections.Generic;
using SampleSolution.Domain.Aggregates;
using SampleSolution.Repositories;

namespace SampleSolution.Services
{
    public class SomeDataReadService : ISomeDataReadService
    {
        private readonly ISomeDataRepository _mySampleSolutionRepository;

        public SomeDataReadService(ISomeDataRepository mySampleSolutionRepository)
        {
            _mySampleSolutionRepository = mySampleSolutionRepository;
        }

        public List<SomeAggregate> GetSomeData(string userEmail)
        {
            return _mySampleSolutionRepository.GetMySampleSolution(userEmail);
        }
    }
}
