using EventDomain.Contracts.Requests;
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
        public List<ParticipantResponse> ParticipantResponse { get; private set; } = new List<ParticipantResponse>();
        public List<ParticipantResponse> AllParticipants { get; private set; } = new List<ParticipantResponse>();

        [BindProperty]
        public Guid SelectedParticipantId { get; set; }

        [BindProperty]
        public string? FirstName { get; set; }
        [BindProperty]
        public string? LastName { get; set; }
        [BindProperty]
        public DateOnly? BirthDate { get; set; }
        [BindProperty]
        public string? Email { get; set; }

        public string? ErrorMessage { get; private set; }

        [BindProperty]
        public Guid EventId { get; set; }

        [BindProperty]
        public string? Title { get; set; }

        [BindProperty]
        public string? Description { get; set; }
        [BindProperty]
        public DateTime? ScheduledTime { get; set; }


        public EventPageModel(ILogger<EventPageModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                EventId = id;

                var response = await _httpClient.GetAsync($"{GlobalParameters.apiUrl}/Events/{EventId}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                EventResponse = JsonConvert.DeserializeObject<EventResponse>(responseBody);

                if (EventResponse == null)
                {
                    ErrorMessage = "There was an error deserializing the event. Please try again later.";
                    return Page();
                }

                // Task

                response = await _httpClient.GetAsync($"{GlobalParameters.apiUrl}/Events/{EventId}/Tasks");

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

                // ParticipantResponse

                response = await _httpClient.GetAsync($"{GlobalParameters.apiUrl}/Events/{id}/Participation");
                response.EnsureSuccessStatusCode();

                string? responseBodyPart = await response.Content.ReadAsStringAsync();

                if (responseBodyPart == null)
                {
                    ErrorMessage = "There was an error fetching the tasks. Please try again later.";
                    return Page();
                }

                List<ParticipantResponse>? deserializedPart = JsonConvert.DeserializeObject<List<ParticipantResponse>>(responseBodyPart);

                if (deserializedPart == null)
                {
                    ErrorMessage = "There was an error deserializing the events. Please try again later.";
                    return Page();
                }

                ParticipantResponse = deserializedPart;

                // Fetch All Participants
                response = await _httpClient.GetAsync($"{GlobalParameters.apiUrl}/Participants");
                response.EnsureSuccessStatusCode();
                string responseBodyAllPart = await response.Content.ReadAsStringAsync();

                List<ParticipantResponse>? deserializedPart2 = JsonConvert.DeserializeObject<List<ParticipantResponse>>(responseBodyAllPart);

                if (deserializedPart2 == null)
                {
                    ErrorMessage = "There was an error deserializing the events. Please try again later.";
                    return Page();
                }

                AllParticipants = deserializedPart2;

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

        public async Task<IActionResult> OnPostAddParticipantAsync()
        {
            try
            {
                var response = await _httpClient.PutAsync($"{GlobalParameters.apiUrl}/Events/{EventId}/Participation/{SelectedParticipantId}", null);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error adding participant to event");
                ErrorMessage = "There was an error adding the participant to the event. Please try again later.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                ErrorMessage = "An unexpected error occurred. Please try again later.";
            }

            return RedirectToPage("/EventPage", new { id = EventId });
        }

        public async Task<IActionResult> OnPostCreateParticipantAsync()
        {
            try
            {
                if (FirstName == null || LastName == null || BirthDate == null || Email == null)
                {
                    ErrorMessage = "Bad input";
                    return Page();
                }

                ParticipantRequest participantRequest = new ParticipantRequest
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    BirthDate = (DateOnly)BirthDate,
                    Email = Email
                };

                var content = new StringContent(JsonConvert.SerializeObject(participantRequest), System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{GlobalParameters.apiUrl}/Participants", content);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error creating participant");
                ErrorMessage = "There was an error creating the participant. Please try again later.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                ErrorMessage = "An unexpected error occurred. Please try again later.";
            }

            return RedirectToPage("/EventPage", new { id = EventId });
        }

        public async Task<IActionResult> OnPostCreateEventFormTaskAsync()
        {
            try
            {
                if (Title == null || Description == null || ScheduledTime == null)
                {
                    ErrorMessage = "Bad input";
                    return Page();
                }

                TaskRequest participantRequest = new TaskRequest
                {
                    Title = Title,
                    Description = Description,
                    ScheduledTime = (DateTime)ScheduledTime,
                    EventId = EventId
                };

                var content = new StringContent(JsonConvert.SerializeObject(participantRequest), System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{GlobalParameters.apiUrl}/Events/{EventId}/Tasks", content);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error creating Task");
                ErrorMessage = "There was an error creating the Task. Please try again later.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                ErrorMessage = "An unexpected error occurred. Please try again later.";
            }

            return RedirectToPage("/EventPage", new { id = EventId });
        }
    }
}
