using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstWebApp.Interfaces;
using MyFirstWebApp.Models;
using System.Threading.Tasks;

namespace MyFirstWebApp.Pages.Projects
{
    public class DeleteModel : PageModel
    {
        private readonly IProjectService _projectService;

        [BindProperty]
        public ProjectModel ProjectModel { get; set; }

        public DeleteModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProjectModel = await _projectService.GetProjectByIdAsync(id.Value);

            if (ProjectModel == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _projectService.DeleteProjectAsync(id.Value);
            return RedirectToPage("./Index");
        }
    }
}