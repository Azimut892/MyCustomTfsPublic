using MyFirstWebApp.Models;

namespace MyFirstWebApp.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetTasksAsync(string userId = null, int? teamId = null, int? projectId = null);
        Task<TaskItem> GetTaskByIdAsync(int id);
        Task<TaskItem> CreateTaskAsync(TaskItem task);
        Task UpdateTaskAsync(TaskItem task);
        Task DeleteTaskAsync(int id);
        Task<int> GetTaskCountAsync(string userId);
        Task<Dictionary<TaskState, int>> GetTaskStatisticsAsync(string userId = null, int? teamId = null);
    }
}
