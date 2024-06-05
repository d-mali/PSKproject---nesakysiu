using EventDomain.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Frontas.Pages
{
    public class EventTaskPageModel : PageModel
    {
        private readonly ILogger<EventTaskPageModel> _logger;
        private readonly HttpClient _httpClient;

        public TaskResponse? TaskResponse { get; private set; }
        public string? ErrorMessage { get; private set; }
        public Guid EventId { get; private set; }

        public EventTaskPageModel(ILogger<EventTaskPageModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> OnGetAsync(Guid id, Guid eventId)
        {
            EventId = eventId;
            try
            {
                var response = await _httpClient.GetAsync($"{GlobalParameters.apiUrl}/Events/{eventId}/Tasks/{id}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                TaskResponse = JsonConvert.DeserializeObject<TaskResponse>(responseBody);

                if (TaskResponse == null)
                {
                    ErrorMessage = "There was an error deserializing the task. Please try again later.";
                    return Page();
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error fetching task from API");
                ErrorMessage = "There was an error fetching the task. Please try again later.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                ErrorMessage = "An unexpected error occurred. Please try again later.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid taskId, [FromForm] Guid eventId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{GlobalParameters.apiUrl}/Events/{eventId}/Tasks/{taskId}");

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Successfully deleted task with ID {TaskId} from event with ID {EventId}", taskId, eventId);
                    return RedirectToPage("/EventPage", new { id = eventId });
                }
                else
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Failed to delete task. Status Code: {StatusCode}, Response: {ResponseContent}", response.StatusCode, responseContent);
                    ErrorMessage = $"There was an error deleting the task. Status Code: {response.StatusCode}, Response: {responseContent}";
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error deleting task from API");
                ErrorMessage = "There was an error deleting the task. Please try again later.";
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
