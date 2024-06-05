using EventDomain.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Frontas.Pages
{
    public class EmployeePageModel : PageModel
    {
        private readonly ILogger<EmployeePageModel> _logger;
        private readonly HttpClient _httpClient;

        public EmployeeResponse? EmployeeResponse { get; private set; }
        public string? ErrorMessage { get; private set; }

        public EventResponse? Event { get; private set; }

        public Guid EventId { get; private set; }

        public EmployeePageModel(ILogger<EmployeePageModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> OnGetAsync(Guid id, Guid eventId)
        {
            try
            {
                EventId = eventId;

                var response = await _httpClient.GetAsync($"{GlobalParameters.apiUrl}/Users/{id}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                EmployeeResponse = JsonConvert.DeserializeObject<EmployeeResponse>(responseBody);

                if (EmployeeResponse == null)
                {
                    ErrorMessage = "There was an error deserializing the Employee. Please try again later.";
                    return Page();
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error fetching Employee from API");
                ErrorMessage = "There was an error fetching the Employee. Please try again later.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                ErrorMessage = "An unexpected error occurred. Please try again later.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid EmployeeId, [FromForm] Guid eventId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{GlobalParameters.apiUrl}/Users/{EmployeeId}");

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Successfully removed Employee with ID {EmployeeId} from event with ID {EventId}", EmployeeId, eventId);
                    return RedirectToPage("/EventPage", new { id = eventId });
                }
                else
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Failed to remove Employee. Status Code: {StatusCode}, Response: {ResponseContent}", response.StatusCode, responseContent);
                    ErrorMessage = $"There was an error removing the Employee from the event. Status Code: {response.StatusCode}, Response: {responseContent}";
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error removing Employee from event");
                ErrorMessage = "There was an error removing the Employee from the event. Please try again later.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                ErrorMessage = "An unexpected error occurred. Please try again later.";
            }

            return Page();
        }
    }
}
