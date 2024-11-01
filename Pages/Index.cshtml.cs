using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManagementApplication.Interfaces;
using TaskManagementApplication.Models;

namespace TaskManagementApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ITaskItemService _taskItemService;

        public IndexModel(ITaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }

        public IList<TaskItem> TaskItems { get; set; }
        public string TitleSort { get; set; }
        public string DueDateSort { get; set; }
        public string ProjectSort { get; set; }
        public string StatusSort { get; set; }
        public string PrioritySort { get; set; }
        public string AssigneeSort { get; set; }
        public string CurrentSort { get; set; }


        public async Task OnGetAsync([FromQuery] string sortOrder)
        {
            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            DueDateSort = sortOrder == "date" ? "date_desc" : "date";
            ProjectSort = sortOrder == "project" ? "project_desc" : "project";
            StatusSort = sortOrder == "status" ? "status_desc" : "status";
            PrioritySort = sortOrder == "priority" ? "priority_desc" : "priority";
            AssigneeSort = sortOrder == "assignee" ? "assignee_desc" : "assignee";
            CurrentSort = sortOrder;

            var tasks = await _taskItemService.GetAllTaskItemsAsync();

            if (User.IsInRole("User"))
            {
                var userId = User.FindFirst("UserId")?.Value; 
                if (int.TryParse(userId, out var parsedUserId))
                {
                    tasks = tasks.Where(t => t.AssigneeId == parsedUserId).ToList();
                }
            }

            TaskItems = sortOrder switch
            {
                "title_desc" => tasks.OrderByDescending(t => t.Title).ToList(),
                "date" => tasks.OrderBy(t => t.DueDate).ToList(),
                "date_desc" => tasks.OrderByDescending(t => t.DueDate).ToList(),
                "project" => tasks.OrderBy(t => t.Project.Name).ToList(),
                "project_desc" => tasks.OrderByDescending(t => t.Project.Name).ToList(),
                "status" => tasks.OrderBy(t => t.Status.Name).ToList(),
                "status_desc" => tasks.OrderByDescending(t => t.Status.Name).ToList(),
                "priority" => tasks.OrderBy(t => t.Priority.Name).ToList(),
                "priority_desc" => tasks.OrderByDescending(t => t.Priority.Name).ToList(),
                "assignee" => tasks.OrderBy(t => t.Assignee.FullName).ToList(),
                "assignee_desc" => tasks.OrderByDescending(t => t.Assignee.FullName).ToList(),
                _ => tasks.OrderBy(t => t.Title).ToList(), 
            };
        }
    }
}