using TaskManagementApplication.Interfaces;
using TaskManagementApplication.Models;

namespace TaskManagementApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _unitOfWork.Users.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _unitOfWork.Users.GetAllAsync();
        }

        public async Task AddUserAsync(User status)
        {
            // Business logic before adding (e.g., validation)
            await _unitOfWork.Users.AddAsync(status);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateUserAsync(User status)
        {
            // Business logic before updating
            _unitOfWork.Users.Update(status);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var status = await _unitOfWork.Users.GetByIdAsync(id);
            if (status != null)
            {
                _unitOfWork.Users.Remove(status);
                await _unitOfWork.CompleteAsync();
            }
            // Handle case when taskItem is null if needed
        }
    }
}