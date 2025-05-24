using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFirstWebApp.Interfaces
{
    public interface IDropdownService
    {
        Task<SelectList> GetUsersSelectListAsync();
        Task<SelectList> GetProjectsSelectListAsync();
        SelectList GetTaskStatesSelectList();
        Task<List<SelectListItem>> GetManagersSelectListAsync();
        Task<List<SelectListItem>> GetMembersSelectListAsync(int? teamId = null);
    }
}