using Microsoft.EntityFrameworkCore;
using TaskManagementApplication.Interfaces;
using TaskManagementApplication.Models;

namespace TaskManagementApplication.Repositories
{
    public class TaskItemRepository : Repository<TaskItem>, ITaskItemRepository
    {
        public TaskItemRepository(TaskContext context) : base(context)
        {
        }

        public override async Task<TaskItem> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(t => t.Project)
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public override async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _dbSet
                .Include(t => t.Project)
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .ToListAsync();
        }

    }
}