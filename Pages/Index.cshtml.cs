using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstWebApp.Interfaces;
using MyFirstWebApp.Models;

namespace MyFirstWebApp.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ITeamService _teamService;
        private readonly ITaskService _taskService;

        public IndexModel(IUserService userService, ITeamService teamService, ITaskService taskService)
        {
            _userService = userService;
            _teamService = teamService;
            _taskService = taskService;
        }

        [BindProperty]
        public string UserName { get; set; }
        public Team Team { get; set; }
        public List<TaskItem> MyTasks { get; set; }
        public List<TaskItem> TeamTasks { get; set; }
        public int MyTaskCount { get; set; }
        public int TeamTaskCount { get; set; }
        public int CompletedTasksCount { get; set; }
        public int InWorkTasksCount { get; set; }


        public async Task OnGetAsync()
        {
            var currentUser = await _userService.GetCurrentUserAsync(User);
            if (currentUser == null) return;

            UserName = currentUser.UserName;

            // Get user's tasks
            MyTasks = await _taskService.GetTasksAsync(userId: currentUser.Id);
            MyTaskCount = MyTasks.Count;

            // Get team related data
            if (currentUser.TeamId != null)
            {
                Team = await _teamService.GetTeamByIdAsync(currentUser.TeamId.Value);

                if (Team != null)
                {
                    TeamTasks = await _taskService.GetTasksAsync(teamId: Team.Id);
                    // Exclude user's own tasks from team tasks
                    TeamTasks = TeamTasks.Where(t => t.AssignedToUserId != currentUser.Id).ToList();
                    TeamTaskCount = TeamTasks.Count;
                }
            }

            // Calculate statistics
            var allRelevantTasks = MyTasks.Concat(TeamTasks ?? new List<TaskItem>());
            CompletedTasksCount = allRelevantTasks.Count(t => t.Status == TaskState.Completed);
            InWorkTasksCount = allRelevantTasks.Count(t => t.Status == TaskState.InWork);
        }
    }
}