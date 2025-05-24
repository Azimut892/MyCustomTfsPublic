using Microsoft.EntityFrameworkCore;
using MyFirstWebApp.Data;
using MyFirstWebApp.Interfaces;
using MyFirstWebApp.Models;

namespace MyFirstWebApp.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetTasksAsync(string userId = null, int? teamId = null, int? projectId = null)
        {
            var query = _context.TaskItems
                .Include(t => t.AssignedToUser)
                .Include(t => t.Team)
                .Include(t => t.Project)
                .AsQueryable();

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(t => t.AssignedToUserId == userId);
            }

            if (teamId.HasValue)
            {
                query = query.Where(t => t.TeamId == teamId);
            }

            if (projectId.HasValue)
            {
                query = query.Where(t => t.ProjectId == projectId);
            }

            return await query.OrderByDescending(t => t.CreatedDate).ToListAsync();
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            return await _context.TaskItems
                .Include(t => t.AssignedToUser)
                .Include(t => t.Team)
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TaskItem> CreateTaskAsync(TaskItem task)
        {
            if (!string.IsNullOrEmpty(task.AssignedToUserId))
            {
                var user = await _context.Users
                    .Include(u => u.Team)
                    .FirstOrDefaultAsync(u => u.Id == task.AssignedToUserId);

                if (user?.Team != null)
                {
                    task.TeamId = user.Team.Id;
                }
            }

            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            if (!string.IsNullOrEmpty(task.AssignedToUserId))
            {
                var user = await _context.Users
                    .Include(u => u.Team)
                    .FirstOrDefaultAsync(u => u.Id == task.AssignedToUserId);

                if (user?.Team != null)
                {
                    task.TeamId = user.Team.Id;
                }
            }

            _context.TaskItems.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task != null)
            {
                _context.TaskItems.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetTaskCountAsync(string userId)
        {
            return await _context.TaskItems
                .Where(t => t.AssignedToUserId == userId)
                .CountAsync();
        }

        public async Task<Dictionary<TaskState, int>> GetTaskStatisticsAsync(string userId = null, int? teamId = null)
        {
            var query = _context.TaskItems.AsQueryable();

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(t => t.AssignedToUserId == userId);
            }

            if (teamId.HasValue)
            {
                query = query.Where(t => t.TeamId == teamId);
            }

            var tasks = await query.ToListAsync();
            
            return tasks.GroupBy(t => t.Status)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
