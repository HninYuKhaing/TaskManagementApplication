using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManagementApplication.Exceptions;
using TaskManagementApplication.Interfaces;
using TaskManagementApplication.Models;
using TaskManagementApplication.Services;

namespace TaskManagementApplication.Pages
{
    public class EditModel : PageModel
    {
        private readonly ITaskItemService _taskItemService;
        private readonly IProjectService _projectService;
        private readonly IStatusService _statusService;
        private readonly IPriorityService _priorityService;
        private readonly IUserService _userService;
        private readonly ILogger<TaskItemService> _logger;
        public EditModel(
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

        public async Task<IActionResult> OnGetAsync(int id)
        {
            TaskItem = await _taskItemService.GetTaskItemByIdAsync(id);

            if (TaskItem == null)
            {
                return RedirectToPage("/Error", new { errorMessage = "The requested task was not found." });

            }

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

            var existingTaskItem = await _taskItemService.GetTaskItemByIdAsync(TaskItem.Id);

            if (existingTaskItem == null)
            {
                return RedirectToPage("/Error", new { errorMessage = "The requested task was not found." });
            }

            if (User.IsInRole("User"))
            {
                existingTaskItem.StatusId = TaskItem.StatusId;
                existingTaskItem.PriorityId = TaskItem.PriorityId;
            }
            else
            {
                existingTaskItem.Title = TaskItem.Title;
                existingTaskItem.Description = TaskItem.Description;
                existingTaskItem.ProjectId = TaskItem.ProjectId;
                existingTaskItem.DueDate = TaskItem.DueDate;
                existingTaskItem.StatusId = TaskItem.StatusId;
                existingTaskItem.PriorityId = TaskItem.PriorityId;
                existingTaskItem.AssigneeId = TaskItem.AssigneeId;
            }

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
                await _taskItemService.UpdateTaskItemAsync(existingTaskItem);
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
                _logger.LogError(ex, "An error occurred while updating a TaskItem.");
                throw;
            }

        }
    }
}