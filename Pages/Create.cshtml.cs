using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManagementApplication.Exceptions;
using TaskManagementApplication.Interfaces;
using TaskManagementApplication.Models;
using TaskManagementApplication.Services;

namespace TaskManagementApplication.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ITaskItemService _taskItemService;
        private readonly IProjectService _projectService;
        private readonly IStatusService _statusService;
        private readonly IPriorityService _priorityService;
        private readonly IUserService _userService;
        private readonly ILogger<TaskItemService> _logger;
        public CreateModel(
            ITaskItemService taskItemService,
            IProjectService projectService,
            IStatusService statusService,
            IPriorityService priorityService,
            IUserService userService,
            ILogger<TaskItemService> logger)
        {
            _taskItemService = taskItemService;
            _projectService = projectService;
            _statusService = statusService;
            _priorityService = priorityService;
            _userService = userService;
            _logger = logger;
        }

        [BindProperty]
        public TaskItem TaskItem { get; set; }

        public SelectList ProjectList { get; set; }
        public SelectList StatusList { get; set; }
        public SelectList PriorityList { get; set; }
        public SelectList UserList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ProjectList = new SelectList(await _projectService.GetAllProjectsAsync(), "Id", "Name");
            StatusList = new SelectList(await _statusService.GetAllStatusesAsync(), "Id", "Name");
            PriorityList = new SelectList(await _priorityService.GetAllPrioritiesAsync(), "Id", "Name");
            UserList = new SelectList(await _userService.GetAllUsersAsync(), "Id", "FullName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("TaskItem.Project");
            ModelState.Remove("TaskItem.Status");
            ModelState.Remove("TaskItem.Priority");
            ModelState.Remove("TaskItem.Assignee");

            if (!ModelState.IsValid)
            {
                ProjectList = new SelectList(await _projectService.GetAllProjectsAsync(), "Id", "Name");
                StatusList = new SelectList(await _statusService.GetAllStatusesAsync(), "Id", "Name");
                PriorityList = new SelectList(await _priorityService.GetAllPrioritiesAsync(), "Id", "Name");
                UserList = new SelectList(await _userService.GetAllUsersAsync(), "Id", "FullName");
                return Page();
            }

            try
            {
                await _taskItemService.AddTaskItemAsync(TaskItem);
                return RedirectToPage("Index");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("TaskItem.DueDate", ex.Message);
                ProjectList = new SelectList(await _projectService.GetAllProjectsAsync(), "Id", "Name");
                StatusList = new SelectList(await _statusService.GetAllStatusesAsync(), "Id", "Name");
                PriorityList = new SelectList(await _priorityService.GetAllPrioritiesAsync(), "Id", "Name");
                UserList = new SelectList(await _userService.GetAllUsersAsync(), "Id", "FullName");
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new TaskItem.");
                throw;
            }
        }
    }
}