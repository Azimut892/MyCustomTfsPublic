using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApp.Data;
using MyFirstWebApp.Interfaces;
using MyFirstWebApp.Models;
using MyFirstWebApp.Services;


namespace MyFirstWebApp.Services
{
    public class DropdownService : IDropdownService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public DropdownService(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<SelectList> GetUsersSelectListAsync()
        {
            var users = await _context.Users.ToListAsync();
            return new SelectList(users, "Id", "UserName");
        }

        public async Task<SelectList> GetProjectsSelectListAsync()
        {
            var projects = await _context.ProjectModel.ToListAsync();
            return new SelectList(projects, "Id", "Name");
        }

        public SelectList GetTaskStatesSelectList()
        {
            var taskStateList = Enum.GetValues(typeof(TaskState))
                .Cast<TaskState>()
                .Select(state => new SelectListItem
                {
                    Value = state.ToString(),
                    Text = EnumService.GetDisplayName(state)
                }).ToList();

            return new SelectList(taskStateList, "Value", "Text");
        }

        public async Task<List<SelectListItem>> GetManagersSelectListAsync()
        {
            // Get all users
            var users = await _context.Users.ToListAsync();
            var result = new List<SelectListItem>();

            // For each user, check if they have the manager role
            foreach (var user in users)
            {
                // Use the UserService to check role
                bool isManager = await _userService.IsInRoleAsync(user.Id, "Manager");

                // Only add users with Manager role
                if (isManager)
                {
                    result.Add(new SelectListItem
                    {
                        Value = user.Id,
                        Text = $"{user.UserName} ({user.Email ?? "No email"})"
                    });
                }
            }

            return result;
        }

        public async Task<List<SelectListItem>> GetMembersSelectListAsync(int? teamId = null)
        {
            var users = await _context.Users.ToListAsync();
            var result = users.Select(u => new SelectListItem
            {
                Value = u.Id,
                Text = $"{u.UserName} ({u.Email ?? "No email"})",
                Selected = teamId.HasValue && u.TeamId == teamId
            }).ToList();

            return result;
        }
    }
}