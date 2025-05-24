using Microsoft.EntityFrameworkCore;
using MyFirstWebApp.Data;
using MyFirstWebApp.Interfaces;
using MyFirstWebApp.Models;

namespace MyFirstWebApp.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _context;

        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectModel>> GetProjectsAsync()
        {
            return await _context.ProjectModel
                .OrderByDescending(p => p.CreatedDate)
                .Select(p => new ProjectModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CreatedDate = p.CreatedDate,
                    Tasks = _context.TaskItems.Where(t => t.ProjectId == p.Id).ToList()
                })
                .ToListAsync();
        }

        public async Task<ProjectModel> GetProjectByIdAsync(int id)
        {
            return await _context.ProjectModel
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ProjectModel> CreateProjectAsync(ProjectModel project)
        {
            _context.ProjectModel.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task UpdateProjectAsync(ProjectModel project)
        {
            _context.Attach(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(int id)
        {
            var project = await _context.ProjectModel.FindAsync(id);
            if (project != null)
            {
                // Update tasks associated with this project
                var tasks = await _context.TaskItems.Where(t => t.ProjectId == id).ToListAsync();
                foreach (var task in tasks)
                {
                    task.ProjectId = null;
                }

                _context.ProjectModel.Remove(project);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetProjectTaskCountAsync(int projectId)
        {
            return await _context.TaskItems
                .Where(t => t.ProjectId == projectId)
                .CountAsync();
        }
    }
}
