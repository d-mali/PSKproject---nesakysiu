using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Frontas.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly HttpClient _httpClient;

        [BindProperty]
        public string? Username { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        public string ErrorMessage { get; private set; }

        public LoginModel(ILogger<LoginModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            ErrorMessage = string.Empty;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var loginRequest = new
            {
                email = Username,
                password = Password
            };

            var content = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("https://localhost:7084/login?useCookies=true&useSessionCookies=false", content);

                if (response.IsSuccessStatusCode)
                {
                    // Redirect to a secure page or dashboard after successful login.
                    return RedirectToPage("/Index");
                }
                else
                {
                    // Handle unsuccessful login
                    ErrorMessage = "Neteisingas vartotojo vardas arba slaptažodis";
                    _logger.LogError("Login failed: {StatusCode}", response.StatusCode);
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error during login request");
                ErrorMessage = "Prisijungimo klaida. Bandykite dar kartą vėliau.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during login");
                ErrorMessage = "Įvyko nenumatyta klaida. Bandykite dar kartą vėliau.";
            }

            return Page();
        }
    }
}
