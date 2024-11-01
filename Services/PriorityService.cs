using TaskManagementApplication.Interfaces;
using TaskManagementApplication.Models;

namespace TaskManagementApplication.Services
{
    public class PriorityService : IPriorityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PriorityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Priority> GetPriorityByIdAsync(int id)
        {
            return await _unitOfWork.Priorities.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Priority>> GetAllPrioritiesAsync()
        {
            return await _unitOfWork.Priorities.GetAllAsync();
        }

        public async Task AddPriorityAsync(Priority priority)
        {
            await _unitOfWork.Priorities.AddAsync(priority);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdatePriorityAsync(Priority priority)
        {
            _unitOfWork.Priorities.Update(priority);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeletePriorityAsync(int id)
        {
            var priority = await _unitOfWork.Priorities.GetByIdAsync(id);
            if (priority != null)
            {
                _unitOfWork.Priorities.Remove(priority);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}