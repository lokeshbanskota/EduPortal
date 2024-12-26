using EduPortal.Authentication;
using Microsoft.AspNetCore.Identity;

namespace EduPortal.Services.Interfaces
{
    public interface IAuthenticateService
    {
        public Task<ApplicationUser> CheckUserAsync(string userName);
        public Task<bool> CheckPasswordAsync(ApplicationUser applicationUser, string password);
        public Task<IList<string>> GetRolesAsync(ApplicationUser applicationUser);
        public Task<IdentityResult> CreateUserAsync(ApplicationUser applicationUser, string password);
        public Task<bool> RoleExistsAsync(string role);
        public Task<IdentityResult> AddToRoleAsync(ApplicationUser applicationUser, string role);
        public Task<IdentityResult> CreateRoleAsync(IdentityRole role);

    }
}
