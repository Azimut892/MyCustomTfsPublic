using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstWebApp.Interfaces;
using MyFirstWebApp.Models;

namespace MyFirstWebApp.Pages.Teams
{
    public class TeamIndexModel : PageModel
    {
        private readonly ITeamService _teamService;
        private readonly IUserService _userService;

        public TeamIndexModel(ITeamService teamService, IUserService userService)
        {
            _teamService = teamService;
            _userService = userService;
        }

        public List<Team> Teams { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = _userService.GetCurrentUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Challenge();
            }

            var isAdmin = await _userService.IsInRoleAsync(userId, "Admin");

            if (isAdmin)
            {
                // Admins see all teams
                Teams = await _teamService.GetTeamsAsync();
            }
            else
            {
                // Users only see their team
                Teams = await _teamService.GetTeamsAsync(userId);
            }

            return Page();
        }
    }
}