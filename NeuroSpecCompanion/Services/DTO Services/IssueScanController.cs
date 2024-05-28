using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NeuroSpec.Shared.Models.DTO;

namespace NeuroSpecCompanion.Services
{
    public class IssueScanService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;

        public IssueScanService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.somee.com/api/IssueScan";
        }

        public async Task<IEnumerable<IssueScan>> GetAllIssueScansAsync()
        {
            var response = await _httpClient.GetAsync(_baseApi);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<IssueScan>>(content);
        }

        public async Task<IssueScan> GetIssueScanByIdAsync(int issueID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/{issueID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IssueScan>(content);
        }

        public async Task<IEnumerable<IssueScan>> GetAllIssueScansByPatientIDAsync(int patientID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/ByPatient/{patientID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<IssueScan>>(content);
        }

        public async Task<IEnumerable<IssueScan>> GetAllIssueScansByPrescriptionIDAsync(int prescriptionID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/ByPrescription/{prescriptionID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<IssueScan>>(content);
        }

        public async Task<IssueScan> InsertIssueScanAsync(IssueScan issueScan)
        {
            var json = JsonSerializer.Serialize(issueScan);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IssueScan>(responseContent);
        }

        public async Task UpdateIssueScanAsync(int issueID, IssueScan issueScan)
        {
            var json = JsonSerializer.Serialize(issueScan);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseApi}/{issueID}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteIssueScanAsync(int issueID)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApi}/{issueID}");
            response.EnsureSuccessStatusCode();
        }
    }
}
