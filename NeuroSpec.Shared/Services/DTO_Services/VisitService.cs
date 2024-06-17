using NeuroSpec.Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NeuroSpecCompanion.Shared.Services.DTO_Services
{
    public class VisitService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;
        private readonly JsonSerializerOptions options;

        public VisitService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.runasp.net/api/visit";
            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        public async Task<List<Visit>> GetAllVisitsAsync()
        {
            var response = await _httpClient.GetAsync(_baseApi);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Visit>>(content, options);
        }

        public async Task<Visit> GetVisitByIDAsync(int visitID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/{visitID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Visit>(content, options);
        }
        public async Task<List<Visit>> GetVisitsByPatientIDAsync(int patientID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/byPatientID/{patientID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Visit>>(content, options);
        }


        public async Task<Visit> InsertVisitAsync(Visit visit)
        {
            var json = JsonSerializer.Serialize(visit, options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Visit>(responseContent, options);
        }

        public async Task UpdateVisitAsync(int visitID, Visit visit)
        {
            var json = JsonSerializer.Serialize(visit, options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseApi}/{visitID}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteVisitAsync(int visitID)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApi}/{visitID}");
            response.EnsureSuccessStatusCode();
        }

        //        [HttpGet("available-time-slots-on-day/{selectedDay}/{DoctorID}")]
        public async Task<List<string>> GetAvailableTimeSlotsOnDayAsync(DateTime selectedDay, int DoctorID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/available-time-slots-on-day/{selectedDay}/{DoctorID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<string>>(content, options);
        }
        //        [HttpGet("byDate/{selectedDay}")]
        public async Task<List<Visit>> GetVisitsByDateAsync(DateTime selectedDay)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/byDate/{selectedDay}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Visit>>(content, options);
        }
        //        [HttpGet("byDoctorID/{doctorID}/onDate/{dateTime}")]

        public async Task<List<Visit>> GetDoctorVisitsOnDate(int doctorID, DateTime dateTime)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/byDoctorID/{doctorID}/onDate/{dateTime}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Visit>>(content, options);
        }

        //        [HttpGet("byDoctorID/{doctorID}")]

        public async Task<List<Visit>> GetDoctorVisits(int doctorID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/byDoctorID/{doctorID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Visit>>(content, options);
        }

        //        [HttpGet("futureVisitsByDoctorID/{doctorID}")]
        public async Task<List<Visit>> GetFutureDoctorVisits(int doctorID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/futureVisitsByDoctorID/{doctorID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Visit>>(content, options);
        }
    }
}
