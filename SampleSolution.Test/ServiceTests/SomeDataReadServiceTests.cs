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
        public void ShouldGetSomeDataOnGetSomeData()
        {
            var userId = Guid.NewGuid().ToString();
            var repositoryWrite = new Mock<ISomeDataWriteRepository>();
            var repositoryRead = new Mock<ISomeDataReadRepository>();
            var service = new SomeDataReadService(repositoryWrite.Object, repositoryRead.Object);

            service.GetSomeData(userId);

            repositoryRead.Verify(r => r.GetSomeData(userId), Times.Once);
        }
    }
}
