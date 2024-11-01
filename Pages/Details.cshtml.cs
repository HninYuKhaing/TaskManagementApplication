using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManagementApplication.Interfaces;
using TaskManagementApplication.Models;

namespace TaskManagementApplication.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ITaskItemService _taskItemService;
        private readonly IProjectService _projectService;
        private readonly IStatusService _statusService;
        private readonly IPriorityService _priorityService;

        public DetailsModel(
            ITaskItemService taskItemService,
            IProjectService projectService,
            IStatusService statusService,
            IPriorityService priorityService)
        {
            _taskItemService = taskItemService;
            _projectService = projectService;
            _statusService = statusService;
            _priorityService = priorityService;
        }

        [BindProperty]
        public TaskItem TaskItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            TaskItem = await _taskItemService.GetTaskItemByIdAsync(id);
            
            if (TaskItem == null)
            {
                return RedirectToPage("/Error", new { errorMessage = "The requested task was not found." });
            }

            return Page();
        }
    }
}