using Microsoft.EntityFrameworkCore;
using MyFirstWebApp.Data;
using MyFirstWebApp.Interfaces;
using MyFirstWebApp.Models;

namespace MyFirstWebApp.Services
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext _context;

        public TeamService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Team>> GetTeamsAsync(string? userId = null)
        {
            var query = _context.Teams
                .Include(t => t.Manager)
                .Include(t => t.Members)
                .AsQueryable();

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(t => t.ManagerId == userId || t.Members.Any(m => m.Id == userId));
            }

            return await query.ToListAsync();
        }

        public async Task<Team?> GetTeamByIdAsync(int id)
        {
            return await _context.Teams
                .Include(t => t.Manager)
                .Include(t => t.Members)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Team> CreateTeamAsync(Team team, string managerId, List<string> memberIds)
        {
            team.ManagerId = managerId;
            
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            if (memberIds != null && memberIds.Any())
            {
                var members = await _context.Users
                    .Where(u => memberIds.Contains(u.Id))
                    .ToListAsync();

                foreach (var member in members)
                {
                    member.TeamId = team.Id;
                }

                await _context.SaveChangesAsync();
            }

            return team;
        }

        public async Task UpdateTeamAsync(Team team, List<string> memberIds)
        {
            // Update team entity
            _context.Attach(team).State = EntityState.Modified;
            
            // Clear existing team members
            var existingMembers = await _context.Users
                .Where(u => u.TeamId == team.Id)
                .ToListAsync();

            foreach (var member in existingMembers)
            {
                member.TeamId = null;
            }

            // Add selected members
            if (memberIds != null && memberIds.Any())
            {
                var newMembers = await _context.Users
                    .Where(u => memberIds.Contains(u.Id))
                    .ToListAsync();

                foreach (var member in newMembers)
                {
                    member.TeamId = team.Id;
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTeamAsync(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team != null)
            {
                // Remove team from members
                var members = await _context.Users.Where(u => u.TeamId == id).ToListAsync();
                foreach (var member in members)
                {
                    member.TeamId = null;
                }

                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Team?> GetUserTeamAsync(string userId)
        {
            var user = await _context.Users
                .Include(u => u.Team)
                .ThenInclude(t => t.Members)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.Team;
        }

        public async Task<Team?> GetManagedTeamAsync(string userId)
        {
            return await _context.Teams
                .Include(t => t.Members)
                .FirstOrDefaultAsync(t => t.ManagerId == userId);
        }
    }
}
