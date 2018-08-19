using SampleSolution.Data.Contexts.Models;
using SampleSolution.Services;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SampleSolution.Test.ServiceTests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task ShouldCreateUserOnCreate()
        {
            var userDb = new List<ApplicationUser>();
            var user = new ApplicationUser
            {
                Id = "someId",
                FirstName = "SomeName",
                Email = "SomeEmail",
                PictureUrl = "SomePictureUrl"
            };
            var store = new Mock<IUserStore<ApplicationUser>>();
            var managerMock = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            managerMock.Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(),It.IsAny<string>()))
                .Callback(() =>
                {
                    userDb.Add(user);
                }).ReturnsAsync(IdentityResult.Success);

            var service = new UserService(managerMock.Object);

            await service.CreateUserAsync(user, "SomePassword");


            Assert.Single(userDb);
            managerMock.Verify(r => r.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Once);
        }
    }

    public class CustomUserManager : UserManager<ApplicationUser>
    {
        public CustomUserManager() : base(new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null)
        { }
    }
}
