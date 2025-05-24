using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyFirstWebApp.Interfaces;
using MyFirstWebApp.Models;

namespace MyFirstWebApp.Pages.Tasks
{
    public class CreateModel : PageModel
    {
        private readonly ITaskService _taskService;
        private readonly IDropdownService _dropdownService;

        [BindProperty]
        public TaskItem TaskItem { get; set; } = new TaskItem();
        public SelectList TaskStates { get; set; }
        public SelectList Users { get; set; }
        public SelectList ProjectModel { get; set; }

        public CreateModel(ITaskService taskService, IDropdownService dropdownService)
        {
            _taskService = taskService;
            _dropdownService = dropdownService;
        }

        public async Task OnGetAsync()
        {
            TaskStates = _dropdownService.GetTaskStatesSelectList();
            Users = await _dropdownService.GetUsersSelectListAsync();
            ProjectModel = await _dropdownService.GetProjectsSelectListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            await _taskService.CreateTaskAsync(TaskItem);
            return RedirectToPage("Index");
        }
    }
}