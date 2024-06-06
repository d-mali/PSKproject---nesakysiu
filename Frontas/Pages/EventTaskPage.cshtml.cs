using EventDomain.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Frontas.Pages
{
    public class EventTaskPageModel : PageModel
    {
        private readonly ILogger<EventTaskPageModel> _logger;
        private readonly HttpClient _httpClient;

        public TaskResponse? TaskResponse { get; private set; }
        public string? ErrorMessage { get; private set; }
        public Guid EventId { get; private set; }
        public List<EmployeeResponse> AllEmployees { get; private set; } = new List<EmployeeResponse>();
        public List<EmployeeResponse> TaskEmployees { get; private set; } = new List<EmployeeResponse>();

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

                // Fetch All Employees
                response = await _httpClient.GetAsync($"{GlobalParameters.apiUrl}/Users");
                response.EnsureSuccessStatusCode();
                string responseBodyAllEmp = await response.Content.ReadAsStringAsync();

                List<EmployeeResponse>? deserializedEmp = JsonConvert.DeserializeObject<List<EmployeeResponse>>(responseBodyAllEmp);

                if (deserializedEmp == null)
                {
                    ErrorMessage = "There was an error deserializing the employees. Please try again later.";
                    return Page();
                }

                AllEmployees = deserializedEmp;

                // Fetch Task Assigned Employees
                TaskEmployees = TaskResponse.Assigned ?? new List<EmployeeResponse>();
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

        public async Task<IActionResult> OnPostAssignTaskAsync(Guid taskId, [FromForm] Guid eventId, [FromForm] Guid userId)
        {
            try
            {
                var response = await _httpClient.PutAsync($"{GlobalParameters.apiUrl}/Users/{userId}/Tasks/{taskId}", null);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/EventTaskPage", new { id = taskId, eventId = eventId });
                }
                else
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Failed to assign task. Status Code: {StatusCode}, Response: {ResponseContent}", response.StatusCode, responseContent);
                    ErrorMessage = $"There was an error assigning the task. Status Code: {response.StatusCode}, Response: {responseContent}";
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error assigning task to user");
                ErrorMessage = "There was an error assigning the task. Please try again later.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                ErrorMessage = "An unexpected error occurred. Please try again later.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveUserAsync(Guid taskId, [FromForm] Guid userId, [FromForm] Guid eventId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{GlobalParameters.apiUrl}/Users/{userId}/Tasks/{taskId}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/EventTaskPage", new { id = taskId, eventId = eventId });
                }
                else
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Failed to remove user from task. Status Code: {StatusCode}, Response: {ResponseContent}", response.StatusCode, responseContent);
                    ErrorMessage = $"There was an error removing the user from the task. Status Code: {response.StatusCode}, Response: {responseContent}";
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error removing user from task");
                ErrorMessage = "There was an error removing the user from the task. Please try again later.";
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
