using TaskManagementApplication.Models;

namespace TaskManagementApplication.Interfaces
{
    public interface IStatusService
    {
        Task<Status> GetStatusByIdAsync(int id);
        Task<IEnumerable<Status>> GetAllStatusesAsync();
        Task AddStatusAsync(Status status);
        Task UpdateStatusAsync(Status status);
        Task DeleteStatusAsync(int id);
    }
}
