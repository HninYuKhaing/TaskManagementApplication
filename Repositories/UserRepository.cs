using Microsoft.EntityFrameworkCore;
using TaskManagementApplication.Interfaces;
using TaskManagementApplication.Models;

namespace TaskManagementApplication.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(TaskContext context) : base(context)
        {
        }

        public override async Task<User> GetByIdAsync(int id)
        {
            return await _dbSet
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbSet
                .ToListAsync();
        }

    }
}