using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyFirstWebApp.Interfaces;
using MyFirstWebApp.Models;
using System.Threading.Tasks;

namespace MyFirstWebApp.Pages.Tasks
{
    public class EditModel : PageModel
    {
        private readonly ITaskService _taskService;
        private readonly IDropdownService _dropdownService;

        [BindProperty]
        public TaskItem TaskItem { get; set; }
        public SelectList TaskStates { get; set; }
        public SelectList Users { get; set; }
        public SelectList ProjectModel { get; set; }

        public EditModel(ITaskService taskService, IDropdownService dropdownService)
        {
            _taskService = taskService;
            _dropdownService = dropdownService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            TaskItem = await _taskService.GetTaskByIdAsync(id);

            if (TaskItem == null)
            {
                return NotFound();
            }

            // Load dropdown data
            TaskStates = _dropdownService.GetTaskStatesSelectList();
            Users = await _dropdownService.GetUsersSelectListAsync();
            ProjectModel = await _dropdownService.GetProjectsSelectListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Reload dropdown data if model is invalid
                TaskStates = _dropdownService.GetTaskStatesSelectList();
                Users = await _dropdownService.GetUsersSelectListAsync();
                ProjectModel = await _dropdownService.GetProjectsSelectListAsync();
            }

            await _taskService.UpdateTaskAsync(TaskItem);
            return RedirectToPage("Index");
        }
    }
}