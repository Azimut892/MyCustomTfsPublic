using Microsoft.AspNetCore.Identity;
using MyFirstWebApp.Models;
using System.Security.Claims;

namespace MyFirstWebApp.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetCurrentUserAsync(ClaimsPrincipal user);
        string GetCurrentUserId(ClaimsPrincipal user);
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<List<UserRoleViewModel>> GetUsersWithRolesAsync();
        Task AssignRoleAsync(string userId, string roleName);
        Task RemoveRoleAsync(string userId, string roleName);
        Task CreateRoleAsync(string roleName);
        Task<List<IdentityRole>> GetRolesAsync();
        Task<bool> RoleExistsAsync(string roleName);
    }
}
