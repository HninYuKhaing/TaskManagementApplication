using Microsoft.EntityFrameworkCore;
using TaskManagementApplication.Interfaces;
using TaskManagementApplication.Models;

namespace TaskManagementApplication.Repositories
{
    public class StatusRepository : Repository<Status>, IStatusRepository
    {
        public StatusRepository(TaskContext context) : base(context)
        {
        }

        public override async Task<Status> GetByIdAsync(int id)
        {
            return await _dbSet
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public override async Task<IEnumerable<Status>> GetAllAsync()
        {
            return await _dbSet
                .ToListAsync();
        }

    }
}