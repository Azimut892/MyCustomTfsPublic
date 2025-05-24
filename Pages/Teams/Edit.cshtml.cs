using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyFirstWebApp.Interfaces;
using MyFirstWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFirstWebApp.Pages.Teams
{
    public class EditModel : PageModel
    {
        private readonly ITeamService _teamService;
        private readonly IDropdownService _dropdownService;

        [BindProperty]
        public Team Team { get; set; }

        [BindProperty]
        public List<string> SelectedMemberIds { get; set; } = new List<string>();

        public List<SelectListItem> AvailableManagers { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> AvailableMembers { get; set; } = new List<SelectListItem>();

        public EditModel(ITeamService teamService, IDropdownService dropdownService)
        {
            _teamService = teamService;
            _dropdownService = dropdownService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Team = await _teamService.GetTeamByIdAsync(id.Value);

            if (Team == null)
            {
                return NotFound();
            }

            AvailableManagers = await _dropdownService.GetManagersSelectListAsync();
            AvailableMembers = await _dropdownService.GetMembersSelectListAsync(Team.Id);

            // Get current member IDs
            SelectedMemberIds = new List<string>();
            if (Team.Members != null)
            {
                foreach (var member in Team.Members)
                {
                    SelectedMemberIds.Add(member.Id);
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                AvailableManagers = await _dropdownService.GetManagersSelectListAsync();
                AvailableMembers = await _dropdownService.GetMembersSelectListAsync(Team.Id);
            }

            await _teamService.UpdateTeamAsync(Team, SelectedMemberIds);
            return RedirectToPage("Index");
        }
    }
}