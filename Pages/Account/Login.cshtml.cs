using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagementApplication.Services;

namespace TaskManagementApplication.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly AuthenticationService _authService;

        public LoginModel(AuthenticationService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public LoginInputModel Input { get; set; }

        public class LoginInputModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _authService.AuthenticateUserAsync(Input.Username, Input.Password);
            if (user != null)
            {
                return RedirectToPage("/Index");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }
    }
}
