using NeuroSpec.Shared.Models.DTO;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NeuroSpec.Shared.Services.DTO_Services
{
    public class BookAppointmentService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;
        private readonly JsonSerializerOptions options;
        public BookAppointmentService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.runasp.net/api/bookappointment";
            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }
        public async Task<BookAppointmentRequest> InsertBookAppointmentRequestAsync(BookAppointmentRequest bookAppointmentRequest)
        {
            var json = JsonSerializer.Serialize(bookAppointmentRequest, options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BookAppointmentRequest>(responseContent, options);
        }
        public async Task<BookAppointmentRequest> GetBookAppointmentRequestByIDAsync(int bookAppointmentRequestID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/{bookAppointmentRequestID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BookAppointmentRequest>(content, options);
        }
        public async Task UpdateBookAppointmentRequestAsync(int bookAppointmentRequestID, BookAppointmentRequest bookAppointmentRequest)
        {
            var json = JsonSerializer.Serialize(bookAppointmentRequest, options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseApi}/{bookAppointmentRequestID}", content);
            response.EnsureSuccessStatusCode();
        }


        //[HttpPut("{bookAppointmentRequestID}/mark-as-confirmed")]
        public async Task MarkAsConfirmedAsync(int bookAppointmentRequestID)
        {
            var response = await _httpClient.PutAsync($"{_baseApi}/{bookAppointmentRequestID}/mark-as-confirmed", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteBookAppointmentRequestAsync(int bookAppointmentRequestID)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApi}/{bookAppointmentRequestID}");
            response.EnsureSuccessStatusCode();
        }

        //        [HttpGet("not-confirmed")]

        public async Task<List<BookAppointmentRequest>> GetNotConfirmedBookAppointmentRequestsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/not-confirmed");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<BookAppointmentRequest>>(content, options);
        }

        //        [HttpGet("by-patient-id/{patientID}")]
        public async Task<List<BookAppointmentRequest>> GetBookAppointmentRequestsByPatientIDAsync(int patientID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/by-patient-id/{patientID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<BookAppointmentRequest>>(content, options);
        }


    }
}
