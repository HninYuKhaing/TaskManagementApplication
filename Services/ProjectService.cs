using TaskManagementApplication.Interfaces;
using TaskManagementApplication.Models;

namespace TaskManagementApplication.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            return await _unitOfWork.Projects.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _unitOfWork.Projects.GetAllAsync();
        }

        public async Task AddProjectAsync(Project project)
        {
            await _unitOfWork.Projects.AddAsync(project);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateProjectAsync(Project project)
        {
            _unitOfWork.Projects.Update(project);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteProjectAsync(int id)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(id);
            if (project != null)
            {
                _unitOfWork.Projects.Remove(project);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}