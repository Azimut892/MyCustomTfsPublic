using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstWebApp.Interfaces;
using MyFirstWebApp.Models;

namespace MyFirstWebApp.Pages.Tasks
{
    public class IndexModel : PageModel
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;
        private readonly ITeamService _teamService;

        public IndexModel(ITaskService taskService, IUserService userService, ITeamService teamService)
        {
            _taskService = taskService;
            _userService = userService;
            _teamService = teamService;
        }

        public List<TaskItem> Tasks { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsManager { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = _userService.GetCurrentUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Challenge();
            }

            IsAdmin = await _userService.IsInRoleAsync(userId, "Admin");
            IsManager = await _userService.IsInRoleAsync(userId, "Manager");

            if (IsAdmin)
            {
                // Admins see all tasks
                Tasks = await _taskService.GetTasksAsync();
            }
            else if (IsManager)
            {
                // Managers see their team's tasks
                var managedTeam = await _teamService.GetManagedTeamAsync(userId);
                if (managedTeam != null)
                {
                    Tasks = await _taskService.GetTasksAsync(teamId: managedTeam.Id);
                }
                else
                {
                    Tasks = new List<TaskItem>();
                }
            }
            else
            {
                // Regular users see only their assigned tasks
                Tasks = await _taskService.GetTasksAsync(userId: userId);
            }

            return Page();
        }
    }
}