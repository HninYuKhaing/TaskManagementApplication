using Microsoft.EntityFrameworkCore;
using TaskManagementApplication.Interfaces;
using TaskManagementApplication.Models;

namespace TaskManagementApplication.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(TaskContext context) : base(context)
        {
        }

        public override async Task<Project> GetByIdAsync(int id)
        {
            return await _dbSet
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public override async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _dbSet
                .ToListAsync();
        }

    }
}