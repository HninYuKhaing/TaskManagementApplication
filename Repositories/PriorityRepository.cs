using Microsoft.EntityFrameworkCore;
using TaskManagementApplication.Interfaces;
using TaskManagementApplication.Models;

namespace TaskManagementApplication.Repositories
{
    public class PriorityRepository : Repository<Priority>, IPriorityRepository
    {
        public PriorityRepository(TaskContext context) : base(context)
        {
        }

        public override async Task<Priority> GetByIdAsync(int id)
        {
            return await _dbSet
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public override async Task<IEnumerable<Priority>> GetAllAsync()
        {
            return await _dbSet
                .ToListAsync();
        }

    }
}