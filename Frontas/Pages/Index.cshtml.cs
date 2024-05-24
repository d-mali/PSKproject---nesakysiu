using EventDomain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Frontas.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly HttpClient _httpClient;

        public List<Event> Event { get; private set; } = new List<Event>(); // Initialize to an empty list
        public string? ErrorMessage { get; private set; }
        public int DaysUntilStart { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task OnGetAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://localhost:7084/api/Event");
                response.EnsureSuccessStatusCode();

                string? responseBody = await response.Content.ReadAsStringAsync();

                if (responseBody == null)
                {
                    ErrorMessage = "There was an error fetching the events. Please try again later.";
                    return;
                }

                List<Event>? deserializedEvent = JsonConvert.DeserializeObject<List<Event>>(responseBody);

                if (deserializedEvent == null)
                {
                    ErrorMessage = "There was an error deserializing the events. Please try again later.";
                    return;
                }

                Event = deserializedEvent;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error fetching events from API");
                ErrorMessage = "There was an error fetching the events. Please try again later.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                ErrorMessage = "An unexpected error occurred. Please try again later.";
            }
        }
    }
}
