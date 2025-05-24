using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstWebApp.Interfaces;
using MyFirstWebApp.Models;
using System.Threading.Tasks;

namespace MyFirstWebApp.Pages.Teams
{
    public class DeleteModel : PageModel
    {
        private readonly ITeamService _teamService;

        [BindProperty]
        public Team Team { get; set; }

        public DeleteModel(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Team = await _teamService.GetTeamByIdAsync(id);

            if (Team == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Team == null || Team.Id == 0)
            {
                return NotFound();
            }

            await _teamService.DeleteTeamAsync(Team.Id);
            return RedirectToPage("./Index");
        }
    }
}