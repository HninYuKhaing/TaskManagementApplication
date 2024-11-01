using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagementApplication.Services;

namespace TaskManagementApplication.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class RegisterModel : PageModel
    {
        private readonly AuthenticationService _authService;

        public RegisterModel(AuthenticationService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; }

        public class RegisterInputModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public int RoleId { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var success = await _authService.RegisterUserAsync(Input.Username, Input.Email, Input.Password, Input.FirstName, Input.LastName, Input.RoleId);
                if (success)
                {
                    return RedirectToPage("/Account/Login");
                }
                ModelState.AddModelError(string.Empty, "User already exists.");
            }
            return Page();
        }
    }
}
