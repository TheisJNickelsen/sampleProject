using SampleSolution.Data.Contexts.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using SampleSolution.Repositories;
using Microsoft.AspNetCore.Http;

namespace SampleSolution.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
        {
            try
            {
                return await _userManager.CreateAsync(user, password);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser userToVerify, string password)
        {
            return await _userManager.CheckPasswordAsync(userToVerify, password);
        }

        public async Task<ApplicationUser> GetCurrentUserAsync(string userId)
        {
            return await _userManager.FindByEmailAsync(userId);
        }
    }
}
