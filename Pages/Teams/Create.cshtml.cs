using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyFirstWebApp.Interfaces;
using MyFirstWebApp.Models;


namespace MyFirstWebApp.Pages.Teams
{
    public class CreateModel : PageModel
    {
        private readonly ITeamService _teamService;
        private readonly IDropdownService _dropdownService;

        public CreateModel(ITeamService teamService, IDropdownService dropdownService)
        {
            _teamService = teamService;
            _dropdownService = dropdownService;
        }

        [BindProperty]
        public Team Team { get; set; } = new Team();

        [BindProperty]
        public string SelectedManagerId { get; set; }

        [BindProperty]
        public List<string> SelectedMemberIds { get; set; } = new List<string>();

        public List<SelectListItem> AvailableManagers { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> AvailableMembers { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGetAsync()
        {
            // Get dropdown data directly as List<SelectListItem>
            AvailableManagers = await _dropdownService.GetManagersSelectListAsync();
            AvailableMembers = await _dropdownService.GetMembersSelectListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
            }

            await _teamService.CreateTeamAsync(Team, SelectedManagerId, SelectedMemberIds);
            return RedirectToPage("./Index");
        }
    }
}