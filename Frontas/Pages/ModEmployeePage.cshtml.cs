
using EventDomain.Contracts.Requests;
using EventDomain.Contracts.Responses;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Frontas.Pages
{
    public class ModEmployeePageModel : PageModel
    {

        /*
         * cia shit nukop[ijuotas, reikia preitaikyti
         */
        private readonly ILogger<ModEventPageModel> _logger;
        private readonly HttpClient _httpClient;

        [BindProperty]
        public EmployeeResponse? EmployeeResponse { get; set; }

        public string? ErrorMessage { get; private set; }

        [BindProperty]
        public Guid EventId { get; private set; }

        public ModEmployeePageModel(ILogger<ModEventPageModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> OnGetAsync(Guid id, Guid eventId)
        {
            if (id == Guid.Empty)
            {
                ErrorMessage = "Invalid employee ID.";
                return Page();
            }

            try
            {
                EventId = eventId;

                var response = await _httpClient.GetAsync($"{GlobalParameters.apiUrl}/Users/{id}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                EmployeeResponse = JsonConvert.DeserializeObject<EmployeeResponse>(responseBody);

                if (EmployeeResponse == null)
                {
                    ErrorMessage = "Employee not found.";
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error fetching employee details from API");
                ErrorMessage = "There was an error fetching the employee details. Please try again later.";
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
            if (EmployeeResponse == null)
            {
                ErrorMessage = "Employee details are missing. Please try again.";
                return Page();
            }

            EventId = eventId;

            EmployeeRequest EmployeeRequest = new EmployeeRequest { FirstName = EmployeeResponse.FirstName, LastName = EmployeeResponse.LastName };

            /*if (!ModelState.IsValid)
            {
                ErrorMessage = "The form contains some invalid data. Please correct it and try again.";
                return Page();
            }*/

            if (EmployeeRequest == null)
            {
                ErrorMessage = "Employee details are missing. Please try again.";
                return Page();
            }

            try
            {
                if (string.IsNullOrEmpty(EmployeeRequest.FirstName) || string.IsNullOrEmpty(EmployeeRequest.LastName))
                {
                    ErrorMessage = "Please provide all required employee details.";
                    return Page();
                }

                _logger.LogInformation("Updating employee with ID {employeeid}", id);

                var jsonContent = JsonConvert.SerializeObject(EmployeeRequest);
                _logger.LogDebug("Serialized Event object: {JsonContent}", jsonContent);

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{GlobalParameters.apiUrl}/Users/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Successfully updated employee with ID {employeeid}", id);
                    //nes ir is vartotojo cia eina
                    //return RedirectToPage("/EmployeePage", new { id = id, eventId = EventId});
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

        public async Task<IActionResult> OnPostDeleteAsync(Guid id, Guid eventId, string requestMethod)
        {
            if (EmployeeResponse == null)
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
                EventId = eventId;

                var response = await _httpClient.DeleteAsync($"{GlobalParameters.apiUrl}/Users/{EmployeeResponse.Id}");

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Successfully deleted employee with ID {EmployeeId}", EmployeeResponse.Id);
                    return RedirectToPage("/EventPage", new { id = EventId });
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
                _logger.LogError(httpEx, "Error deleting employee from API");
                ErrorMessage = "There was an error deleting the employee. Please try again later.";
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
