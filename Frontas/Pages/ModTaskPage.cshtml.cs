using EventDomain.Contracts.Requests;
using EventDomain.Contracts.Responses;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Frontas.Pages
{
    public class ModTaskPageModel : PageModel
    {
        private readonly ILogger<ModTaskPageModel> _logger;
        private readonly HttpClient _httpClient;

        [BindProperty]
        public TaskResponse? TaskResponse { get; set; }

        public string? ErrorMessage { get; private set; }

        public ModTaskPageModel(ILogger<ModTaskPageModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> OnGetAsync(Guid id, Guid eventId)
        {
            if (id == Guid.Empty)
            {
                ErrorMessage = "Invalid event ID.";
                return Page();
            }

            try
            {
                var response = await _httpClient.GetAsync($"{GlobalParameters.apiUrl}/Events/{eventId}/Tasks/{id}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                TaskResponse = JsonConvert.DeserializeObject<TaskResponse>(responseBody);

                if (TaskResponse == null)
                {
                    ErrorMessage = "Task not found.";
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error fetching event details from API");
                ErrorMessage = "There was an error fetching the task details. Please try again later.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                ErrorMessage = "An unexpected error occurred. Please try again later.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id, Guid eventId)
        {

            if (TaskResponse == null)
            {
                ErrorMessage = "Task details are missing. Please try again.";
                return Page();
            }

            TaskRequest TaskRequest = new TaskRequest
            {
                Title = TaskResponse.Title,
                Description = TaskResponse.Description,
                ScheduledTime = TaskResponse.ScheduledTime,
                Status = TaskResponse.Status
            };

            /*if (!ModelState.IsValid)
            {
                ErrorMessage = "The form contains some invalid data. Please correct it and try again.";
                return Page();
            }*/

            if (TaskRequest == null)
            {
                ErrorMessage = "Task details are missing. Please try again.";
                return Page();
            }

            try
            {
                if (string.IsNullOrEmpty(TaskRequest.Title) || string.IsNullOrEmpty(TaskRequest.Description) || TaskRequest.ScheduledTime == default)
                {
                    ErrorMessage = "Please provide all required task details.";
                    return Page();
                }

                _logger.LogInformation("Updating task with ID {TaskId}", id);

                var jsonContent = JsonConvert.SerializeObject(TaskRequest);
                _logger.LogDebug("Serialized Event object: {JsonContent}", jsonContent);

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{GlobalParameters.apiUrl}/Events/{eventId}/Tasks/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Successfully updated task with ID {TaskId}", id);
                    return RedirectToPage("/EventTaskPage", new { id = id });
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

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            if (TaskResponse == null)
            {
                ErrorMessage = "Task details are missing. Please try again.";
                return Page();
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"{GlobalParameters.apiUrl}/Events/{TaskResponse.EventId}/Tasks/{TaskResponse.Id}");

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Successfully deleted task with ID {TaskId}", TaskResponse.Id);
                    return RedirectToPage("/EventPage", new { id = TaskResponse.EventId });
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
