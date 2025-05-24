using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstWebApp.Interfaces;
using MyFirstWebApp.Models;
using System.Threading.Tasks;

namespace MyFirstWebApp.Pages.Tasks
{
    public class DeleteModel : PageModel
    {
        private readonly ITaskService _taskService;

        [BindProperty]
        public TaskItem TaskItem { get; set; }

        public DeleteModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            TaskItem = await _taskService.GetTaskByIdAsync(id);

            if (TaskItem == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return RedirectToPage("Index");
        }
    }
}