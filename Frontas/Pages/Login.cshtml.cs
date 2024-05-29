using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Frontas.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;

        [BindProperty]
        public string? Username { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        public string ErrorMessage { get; private set; }

        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;
            ErrorMessage = string.Empty;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // For demonstration purposes, using hardcoded username and password.
            // Replace this with your actual authentication logic.
            if (Username == "admin" && Password == "password")
            {
                // Redirect to a secure page or dashboard after successful login.
                return RedirectToPage("/Index");
            }
            else
            {
                ErrorMessage = "Neteisingas vartotojo vardas arba slaptažodis";
                return Page();
            }
        }
    }
}
