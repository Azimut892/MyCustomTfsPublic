using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstWebApp.Interfaces;
using MyFirstWebApp.Models;
using System.Threading.Tasks;

namespace MyFirstWebApp.Pages.Tasks
{
    public class DetailsModel : PageModel
    {
        private readonly ITaskService _taskService;

        public DetailsModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public TaskItem TaskItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TaskItem = await _taskService.GetTaskByIdAsync(id.Value);

            if (TaskItem == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}