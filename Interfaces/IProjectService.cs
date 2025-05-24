using MyFirstWebApp.Models;

namespace MyFirstWebApp.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectModel>> GetProjectsAsync();
        Task<ProjectModel> GetProjectByIdAsync(int id);
        Task<ProjectModel> CreateProjectAsync(ProjectModel project);
        Task UpdateProjectAsync(ProjectModel project);
        Task DeleteProjectAsync(int id);
        Task<int> GetProjectTaskCountAsync(int projectId);
    }
}
