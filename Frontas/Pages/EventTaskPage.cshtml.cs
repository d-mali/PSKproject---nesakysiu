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

        public EventTaskPageModel(ILogger<EventTaskPageModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{GlobalParameters.apiUrl}/Tasks/{id}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                TaskResponse = JsonConvert.DeserializeObject<TaskResponse>(responseBody);

                if (TaskResponse == null)
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
