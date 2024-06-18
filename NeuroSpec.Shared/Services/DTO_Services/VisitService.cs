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
            _baseApi = "http://neurospec.runasp.net/api/Visit";
            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<Visit>> GetAllVisitsByPatientIDAsync(int patientID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/byPatientID/{patientID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Visit>>(content, options);
        }

        public async Task<Visit> InsertVisitAsync(Visit visit)
        {
            var json = JsonSerializer.Serialize(visit);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Visit>(responseContent, options);
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

        public async Task<Visit> GetVisitByIDAsync(int visitID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/{visitID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Visit>(content, options);
        }

        public async Task<List<string>> GetAvailableTimeSlotsOnDayAsync(DateTime selectedDay, int doctorID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/available-time-slots-on-day/{selectedDay.ToString("yyyy-MM-dd")}/{doctorID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<string>>(content, options);
        }

        public async Task<List<Visit>> GetVisitsByDateAsync(DateTime selectedDay)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/byDate/{selectedDay.ToString("yyyy-MM-dd")}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Visit>>(content, options);
        }

        public async Task<List<Visit>> GetDoctorVisitsOnDateAsync(int doctorID, DateTime dateTime)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/byDoctorID/{doctorID}/onDate/{dateTime.ToString("yyyy-MM-dd")}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Visit>>(content, options);
        }

        public async Task<List<Visit>> GetDoctorVisitsAsync(int doctorID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/byDoctorID/{doctorID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Visit>>(content, options);
        }

        public async Task<List<Visit>> GetFutureDoctorVisitsAsync(int doctorID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/futureVisitsByDoctorID/{doctorID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Visit>>(content, options);
        }

    }
}
