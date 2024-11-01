using TaskManagementApplication.Models;

namespace TaskManagementApplication.Interfaces
{
    public interface ITaskItemService
    {
        Task<TaskItem> GetTaskItemByIdAsync(int id);
        Task<IEnumerable<TaskItem>> GetAllTaskItemsAsync();
        Task AddTaskItemAsync(TaskItem taskItem);
        Task UpdateTaskItemAsync(TaskItem taskItem);
        Task DeleteTaskItemAsync(int id);
    }
}
