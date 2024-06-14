using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NeuroSpec.Shared.Models.DTO;

namespace NeuroSpecCompanion.Shared.Services.DTO_Services
{
    public class VisitService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;

        public VisitService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.somee.com/api/Visit";
        }

        public async Task<IEnumerable<Visit>> GetAllVisitsAsync()
        {
            var response = await _httpClient.GetAsync(_baseApi);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Visit>>(content);
        }

        public async Task<Visit> GetVisitByIDAsync(int visitID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/{visitID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Visit>(content);
        }
        public async Task<List<Visit>> GetVisitsByPatientIDAsync(int patientID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/byPatientID/{patientID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Visit>>(content);
        }


        public async Task<Visit> InsertVisitAsync(Visit visit)
        {
            var json = JsonSerializer.Serialize(visit);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Visit>(responseContent);
        }

        public async Task UpdateVisitAsync(int visitID, Visit visit)
        {
            var json = JsonSerializer.Serialize(visit);
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
            return JsonSerializer.Deserialize<List<string>>(content);
        }
    }
}
