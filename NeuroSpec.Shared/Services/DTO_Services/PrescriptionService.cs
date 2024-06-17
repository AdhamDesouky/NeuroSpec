using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NeuroSpec.Shared.Models.DTO;

namespace NeuroSpecCompanion.Shared.Services.DTO_Services
{
    public class PrescriptionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;

        public PrescriptionService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.runasp.net/api/Prescription";
        }

        public async Task<List<Prescription>> GetAllPrescriptionsAsync()
        {
            var response = await _httpClient.GetAsync(_baseApi);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Prescription>>(content);
        }

        public async Task<Prescription> GetPrescriptionByIDAsync(int prescriptionID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/{prescriptionID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Prescription>(content);
        }

        public async Task<List<Prescription>> GetAllPrescriptionsByPatientIDAsync(int patientID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/patient/{patientID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Prescription>>(content);
        }

        public async Task<List<Prescription>> GetAllPrescriptionsByVisitIDAsync(int visitID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/visit/{visitID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Prescription>>(content);
        }

        public async Task<Prescription> InsertPrescriptionAsync(Prescription prescription)
        {
            var json = JsonSerializer.Serialize(prescription);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Prescription>(responseContent);
        }

        public async Task UpdatePrescriptionAsync(int prescriptionID, Prescription prescription)
        {
            var json = JsonSerializer.Serialize(prescription);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseApi}/{prescriptionID}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePrescriptionAsync(int prescriptionID)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApi}/{prescriptionID}");
            response.EnsureSuccessStatusCode();
        }
    }
}
