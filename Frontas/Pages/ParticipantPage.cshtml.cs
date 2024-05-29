using EventDomain.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;


namespace Frontas.Pages
{
    public class ParticipantPageModel : PageModel
    {
        private readonly ILogger<EventPageModel> _logger;
        private readonly HttpClient _httpClient;

        //public List<User> Users { get; private set; } = new List<User>();
        //public List<EventTask> EventTasks { get; private set; } = new List<EventTask>();

        public ParticipantResponse? ParticipantResponse { get; private set; }
        public string? ErrorMessage { get; private set; }

        public EventResponse? Event { get; private set; }

        public ParticipantPageModel(ILogger<EventPageModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{GlobalParameters.apiUrl}/Events/{id}/Participants/{id}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                ParticipantResponse = JsonConvert.DeserializeObject<ParticipantResponse>(responseBody);

                if (ParticipantResponse == null)
                {
                    ErrorMessage = "There was an error deserializing the event. Please try again later.";
                    return Page();
                }
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
