using EventDomain.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Frontas.Pages
{
    public class ParticipantPageModel : PageModel
    {
        private readonly ILogger<ParticipantPageModel> _logger;
        private readonly HttpClient _httpClient;

        public ParticipantResponse? ParticipantResponse { get; private set; }
        public string? ErrorMessage { get; private set; }

        public EventResponse? Event { get; private set; }

        public ParticipantPageModel(ILogger<ParticipantPageModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> OnGetAsync(Guid id, Guid eventId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{GlobalParameters.apiUrl}/Participants/{id}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                ParticipantResponse = JsonConvert.DeserializeObject<ParticipantResponse>(responseBody);

                if (ParticipantResponse == null)
                {
                    ErrorMessage = "There was an error deserializing the participant. Please try again later.";
                    return Page();
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error fetching participant from API");
                ErrorMessage = "There was an error fetching the participant. Please try again later.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                ErrorMessage = "An unexpected error occurred. Please try again later.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid participantId, [FromForm] Guid eventId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{GlobalParameters.apiUrl}/Events/{eventId}/Participation/{participantId}");

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Successfully removed participant with ID {ParticipantId} from event with ID {EventId}", participantId, eventId);
                    return RedirectToPage("/EventPage", new { id = eventId });
                }
                else
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Failed to remove participant. Status Code: {StatusCode}, Response: {ResponseContent}", response.StatusCode, responseContent);
                    ErrorMessage = $"There was an error removing the participant from the event. Status Code: {response.StatusCode}, Response: {responseContent}";
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error removing participant from event");
                ErrorMessage = "There was an error removing the participant from the event. Please try again later.";
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
