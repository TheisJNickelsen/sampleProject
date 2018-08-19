using SampleSolution.Data.Contexts.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SampleSolution.Services
{
    public interface IUserService
    {
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<ApplicationUser> FindByNameAsync(string userName);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(ApplicationUser userToVerify, string password);
        Task<ApplicationUser> GetCurrentUserAsync(string userId);
    }
}
