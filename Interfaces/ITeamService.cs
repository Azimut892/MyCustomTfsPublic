using MyFirstWebApp.Models;

namespace MyFirstWebApp.Interfaces
{
    public interface ITeamService
    {
        Task<List<Team>> GetTeamsAsync(string userId = null);
        Task<Team> GetTeamByIdAsync(int id);
        Task<Team> CreateTeamAsync(Team team, string managerId, List<string> memberIds);
        Task UpdateTeamAsync(Team team, List<string> memberIds);
        Task DeleteTeamAsync(int id);
        Task<Team> GetUserTeamAsync(string userId);
        Task<Team> GetManagedTeamAsync(string userId);
    }
}
