using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstWebApp.Interfaces;
using MyFirstWebApp.Models;

namespace MyFirstWebApp.Pages.Projects
{
    public class CreateModel : PageModel
    {
        private readonly IProjectService _projectService;

        public CreateModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ProjectModel ProjectModel { get; set; } = new ProjectModel();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _projectService.CreateProjectAsync(ProjectModel);
            return RedirectToPage("./Index");
        }
    }
}