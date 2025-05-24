using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstWebApp.Interfaces;
using MyFirstWebApp.Models;

namespace MyFirstWebApp.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class RoleManagementModel : PageModel
    {
        private readonly IUserService _userService;

        public RoleManagementModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string NewRoleName { get; set; }

        public List<IdentityRole> Roles { get; set; }
        public List<UserRoleViewModel> Users { get; set; }

        public async Task OnGetAsync()
        {
            Roles = await _userService.GetRolesAsync();
            Users = await _userService.GetUsersWithRolesAsync();
        }

        public async Task<IActionResult> OnPostCreateRoleAsync()
        {
            if (!string.IsNullOrWhiteSpace(NewRoleName))
            {
                await _userService.CreateRoleAsync(NewRoleName);
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAssignRoleAsync(string userId, string roleName)
        {
            await _userService.AssignRoleAsync(userId, roleName);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveRoleAsync(string userId, string roleName)
        {
            await _userService.RemoveRoleAsync(userId, roleName);
            return RedirectToPage();
        }
    }
}