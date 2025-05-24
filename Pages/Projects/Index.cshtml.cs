using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstWebApp.Interfaces;
using MyFirstWebApp.Models;

namespace MyFirstWebApp.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly IProjectService _projectService;

        public IndexModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public List<ProjectModel> Projects { get; set; }

        public async Task OnGetAsync()
        {
            Projects = await _projectService.GetProjectsAsync();
        }
    }
}