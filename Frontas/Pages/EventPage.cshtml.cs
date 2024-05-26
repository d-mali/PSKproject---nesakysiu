using EventDomain.Contracts.Responses;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Frontas.Pages
{
    public class EventPageModel : PageModel
    {
        private readonly ILogger<EventPageModel> _logger;
        private readonly HttpClient _httpClient;

        //public List<User> Users { get; private set; } = new List<User>();
        //public List<EventTask> EventTasks { get; private set; } = new List<EventTask>();

        public EventResponse? EventResponse { get; private set; }

        public List<TaskResponse> TaskResponse { get; private set; } = new List<TaskResponse>();
        public string? ErrorMessage { get; private set; }

        public EventPageModel(ILogger<EventPageModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{GlobalParameters.apiUrl}/Events/{id}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                EventResponse = JsonConvert.DeserializeObject<EventResponse>(responseBody);

                if (EventResponse == null)
                {
                    ErrorMessage = "There was an error deserializing the event. Please try again later.";
                    return Page();
                }

                response = await _httpClient.GetAsync($"{GlobalParameters.apiUrl}/Tasks?eventId={id}");

                string? responseBodyTasks = await response.Content.ReadAsStringAsync();

                if (responseBodyTasks == null)
                {
                    ErrorMessage = "There was an error fetching the tasks. Please try again later.";
                    return Page();
                }

                List<TaskResponse>? deserializedEvent = JsonConvert.DeserializeObject<List<TaskResponse>>(responseBodyTasks);

                if (deserializedEvent == null)
                {
                    ErrorMessage = "There was an error deserializing the events. Please try again later.";
                    return Page();
                }

                TaskResponse = deserializedEvent;

            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error fetching event from API");
                ErrorMessage = "There was an error fetching the event. Please try again later.";
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
