using EduPortal.Authentication;
using EduPortal.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EduPortal.Services.Implementations
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthenticateService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> AddToRoleAsync(ApplicationUser applicationUser, string role)
        {
            return await _userManager.AddToRoleAsync(applicationUser, role);
        }

        public Task<bool> CheckPasswordAsync(ApplicationUser applicationUser, string password)
        {
            return _userManager.CheckPasswordAsync(applicationUser, password);
        }

        public async Task<ApplicationUser> CheckUserAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<IdentityResult> CreateRoleAsync(IdentityRole role)
        {
            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> CreateUserAsync(ApplicationUser applicationUser, string password)
        {
            return await _userManager.CreateAsync(applicationUser, password);
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser applicationUser)
        {
            return await _userManager.GetRolesAsync(applicationUser);
        }

        public async Task<bool> RoleExistsAsync(string role)
        {
            return await _roleManager.RoleExistsAsync(role);
        }

    }
}
