using TaskManagementApplication.Models;

namespace TaskManagementApplication.Interfaces
{
    public interface IPriorityService
    {
        Task<Priority> GetPriorityByIdAsync(int id);
        Task<IEnumerable<Priority>> GetAllPrioritiesAsync();
        Task AddPriorityAsync(Priority priority);
        Task UpdatePriorityAsync(Priority priority);
        Task DeletePriorityAsync(int id);
    }
}
