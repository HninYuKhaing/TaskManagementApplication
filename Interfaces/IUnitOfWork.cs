namespace TaskManagementApplication.Interfaces
{
    public interface IUnitOfWork
    {
        ITaskItemRepository TaskItems { get; }
        IProjectRepository Projects { get; }
        IStatusRepository Statuses { get; }
        IPriorityRepository Priorities { get; }
        IUserRepository Users { get; }

        Task<int> CompleteAsync();
    }
}
