using TaskManagementApplication.Interfaces;
using TaskManagementApplication.Models;

namespace TaskManagementApplication.Services
{
    public class StatusService : IStatusService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatusService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Status> GetStatusByIdAsync(int id)
        {
            return await _unitOfWork.Statuses.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Status>> GetAllStatusesAsync()
        {
            return await _unitOfWork.Statuses.GetAllAsync();
        }

        public async Task AddStatusAsync(Status status)
        {
            await _unitOfWork.Statuses.AddAsync(status);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateStatusAsync(Status status)
        {
            _unitOfWork.Statuses.Update(status);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteStatusAsync(int id)
        {
            var status = await _unitOfWork.Statuses.GetByIdAsync(id);
            if (status != null)
            {
                _unitOfWork.Statuses.Remove(status);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}