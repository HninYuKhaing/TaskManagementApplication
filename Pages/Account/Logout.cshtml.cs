using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagementApplication.Services;

namespace TaskManagementApplication.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly AuthenticationService _authService;

        public LogoutModel(AuthenticationService authService)
        {
            _authService = authService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await _authService.LogoutAsync();
            return RedirectToPage("/Index");
        }
    }
}
