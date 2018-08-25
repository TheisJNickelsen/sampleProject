using System;
using SampleSolution.Repositories;
using SampleSolution.Services;
using Moq;
using Xunit;

namespace SampleSolution.Test.ServiceTests
{
    public class MySampleSolutionReadServiceTests
    {
        [Fact]
        public void ShouldGetMySampleSolutionOnGetMySampleSolution()
        {
            var userId = Guid.NewGuid().ToString();
            var repository = new Mock<ISomeDataWriteRepository>();
            var service = new SomeDataReadService(repository.Object);

            service.GetSomeData(userId);

            repository.Verify(r => r.GetSomeData(userId), Times.Once);
        }
    }
}
