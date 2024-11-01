using TaskManagementApplication.Interfaces;
using TaskManagementApplication.Models;
using TaskManagementApplication.Repositories;

namespace TaskManagementApplication.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskContext _context;
        public ITaskItemRepository TaskItems { get; private set; }
        public IProjectRepository Projects { get; private set; }
        public IStatusRepository Statuses { get; private set; }
        public IPriorityRepository Priorities { get; private set; }
        public IUserRepository Users { get; private set; }

        public UnitOfWork(TaskContext context)
        {
            _context = context;
            TaskItems = new TaskItemRepository(_context);
            Projects = new ProjectRepository(_context);
            Statuses = new StatusRepository(_context);
            Priorities = new PriorityRepository(_context);
            Users = new UserRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}