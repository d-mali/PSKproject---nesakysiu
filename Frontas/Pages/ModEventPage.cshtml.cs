using EventDomain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Frontas.Pages
{
    public class ModEventPageModel : PageModel
    {
        private readonly ILogger<ModEventPageModel> _logger;
        private readonly HttpClient _httpClient;

        [BindProperty]
        public Event? Event { get; set; }

        public string? ErrorMessage { get; private set; }

        public ModEventPageModel(ILogger<ModEventPageModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                ErrorMessage = "Invalid event ID.";
                return Page();
            }

            try
            {
                var response = await _httpClient.GetAsync($"{GlobalParameters.apiUrl}/Events/{id}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                Event = JsonConvert.DeserializeObject<Event>(responseBody);

                if (Event == null)
                {
                    ErrorMessage = "Event not found.";
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error fetching event details from API");
                ErrorMessage = "There was an error fetching the event details. Please try again later.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                ErrorMessage = "An unexpected error occurred. Please try again later.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "The form contains some invalid data. Please correct it and try again.";
                return Page();
            }

            if (Event == null)
            {
                ErrorMessage = "Event details are missing. Please try again.";
                return Page();
            }

            try
            {
                if (string.IsNullOrEmpty(Event.Title) || string.IsNullOrEmpty(Event.Description) || Event.StartDate == default || Event.EndDate == default)
                {
                    ErrorMessage = "Please provide all required event details.";
                    return Page();
                }

                _logger.LogInformation("Updating event with ID {EventId}", Event.Id);

                var jsonContent = JsonConvert.SerializeObject(Event);
                _logger.LogDebug("Serialized Event object: {JsonContent}", jsonContent);

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{GlobalParameters.apiUrl}/Events/{Event.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Successfully updated event with ID {EventId}", Event.Id);
                    return RedirectToPage("/EventPage", new { id = Event.Id });
                }
                else
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Failed to update event. Status Code: {StatusCode}, Response: {ResponseContent}", response.StatusCode, responseContent);
                    ErrorMessage = $"There was an error updating the event details. Status Code: {response.StatusCode}, Response: {responseContent}";
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error updating event details to API");
                ErrorMessage = "There was an error updating the event details. Please try again later.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                ErrorMessage = "An unexpected error occurred. Please try again later.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            if (Event == null)
            {
                ErrorMessage = "Event details are missing. Please try again.";
                return Page();
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"{GlobalParameters.apiUrl}/Events/{Event.Id}");

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Successfully deleted event with ID {EventId}", Event.Id);
                    return RedirectToPage("/Index");
                }
                else
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Failed to delete event. Status Code: {StatusCode}, Response: {ResponseContent}", response.StatusCode, responseContent);
                    ErrorMessage = $"There was an error deleting the event. Status Code: {response.StatusCode}, Response: {responseContent}";
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error deleting event from API");
                ErrorMessage = "There was an error deleting the event. Please try again later.";
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
