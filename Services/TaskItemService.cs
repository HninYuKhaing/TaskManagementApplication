using TaskManagementApplication.Exceptions;
using TaskManagementApplication.Interfaces;
using TaskManagementApplication.Models;

namespace TaskManagementApplication.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TaskItemService> _logger;


        public TaskItemService(IUnitOfWork unitOfWork, ILogger<TaskItemService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<TaskItem> GetTaskItemByIdAsync(int id)
        {
            try
            {
                return await _unitOfWork.TaskItems.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching TaskItem with ID {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<TaskItem>> GetAllTaskItemsAsync()
        {
            try
            {
                return await _unitOfWork.TaskItems.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all TaskItems");
                throw;
            }
        }

        public async Task AddTaskItemAsync(TaskItem taskItem)
        {
            try
            {
                if (taskItem.DueDate.HasValue && taskItem.DueDate.Value.Date <= DateTime.Today)
                {
                    throw new ValidationException("Due Date must be greater than today.");
                }
                await _unitOfWork.TaskItems.AddAsync(taskItem);
                await _unitOfWork.CompleteAsync();
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validation failed for TaskItem with ID {TaskItemId}.", taskItem.Id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding TaskItem with ID {TaskItemId}.", taskItem.Id);
                throw;
            }
        }

        public async Task UpdateTaskItemAsync(TaskItem taskItem)
        {
            try
            {
                if (taskItem.DueDate.HasValue && taskItem.DueDate.Value.Date <= DateTime.Today)
                {
                    throw new ValidationException("Due Date must be greater than today.");
                }
                _unitOfWork.TaskItems.Update(taskItem);
                await _unitOfWork.CompleteAsync();
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validation failed while updating for TaskItem with ID {TaskItemId}.", taskItem.Id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating TaskItem with ID {TaskItemId}.", taskItem.Id);
                throw;
            }
        }

        public async Task DeleteTaskItemAsync(int id)
        {
            try
            {
                var taskItem = await _unitOfWork.TaskItems.GetByIdAsync(id);
                if (taskItem != null)
                {
                    _unitOfWork.TaskItems.Remove(taskItem);
                    await _unitOfWork.CompleteAsync();
                }
                else
                {
                    _logger.LogWarning("TaskItem with ID: {Id} was not found for deletion", id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting TaskItem with ID: {Id}", id);
                throw;
            }

        }
    }
}