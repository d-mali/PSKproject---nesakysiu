
using EventDomain.Contracts.Requests;
using EventDomain.Contracts.Responses;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Frontas.Pages
{
    public class ModParticipantPageModel : PageModel
    {

        /*
         * cia shit nukop[ijuotas, reikia preitaikyti
         */
        private readonly ILogger<ModEventPageModel> _logger;
        private readonly HttpClient _httpClient;

        [BindProperty]
        public ParticipantResponse? ParticipantResponse { get; set; }

        public string? ErrorMessage { get; private set; }

        public ModParticipantPageModel(ILogger<ModEventPageModel> logger, IHttpClientFactory httpClientFactory)
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

                var response = await _httpClient.GetAsync($"{GlobalParameters.apiUrl}/Participants/{id}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                ParticipantResponse = JsonConvert.DeserializeObject<ParticipantResponse>(responseBody);

                if (ParticipantResponse == null)
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

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (ParticipantResponse == null)
            {
                ErrorMessage = "Event details are missing. Please try again.";
                return Page();
            }

            ParticipantRequest ParticipantRequest = new ParticipantRequest { FirstName = ParticipantResponse.FirstName, LastName = ParticipantResponse.LastName, BirthDate = ParticipantResponse.BirthDate, Email = ParticipantResponse.Email };

            /*if (!ModelState.IsValid)
            {
                ErrorMessage = "The form contains some invalid data. Please correct it and try again.";
                return Page();
            }*/

            if (ParticipantRequest == null)
            {
                ErrorMessage = "Event details are missing. Please try again.";
                return Page();
            }

            try
            {
                if (string.IsNullOrEmpty(ParticipantRequest.FirstName) || string.IsNullOrEmpty(ParticipantRequest.LastName) || ParticipantRequest.BirthDate == default || ParticipantRequest.Email == default)
                {
                    ErrorMessage = "Please provide all required event details.";
                    return Page();
                }

                _logger.LogInformation("Updating participant with ID {participantid}", id);

                var jsonContent = JsonConvert.SerializeObject(ParticipantRequest);
                _logger.LogDebug("Serialized Event object: {JsonContent}", jsonContent);

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{GlobalParameters.apiUrl}/Participants/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Successfully updated participant with ID {participantid}", id);
                    //todo : redirect to event page
                    return RedirectToPage("/index");
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

        public async Task<IActionResult> OnPostDeleteAsync(Guid id, string requestMethod)
        {
            if (ParticipantResponse == null)
            {
                ErrorMessage = "Event details are missing. Please try again.";
                return Page();
            }

            if (requestMethod != "DELETE")
            {
                ErrorMessage = "Invalid request method.";
                return Page();
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"{GlobalParameters.apiUrl}/Participants/{ParticipantResponse.Id}");

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Successfully deleted event with ID {EventId}", ParticipantResponse.Id);
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
