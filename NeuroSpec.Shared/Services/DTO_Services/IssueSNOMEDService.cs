using NeuroSpec.Shared.Models.DTO;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NeuroSpecCompanion.Shared.Services.DTO_Services
{
    public class IssueSNOMEDService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;
        private readonly JsonSerializerOptions _options;

        public IssueSNOMEDService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.runasp.net/api/IssueSNOMED";
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<IssueSNOMED>> GetAllIssuesAsync()
        {
            var response = await _httpClient.GetAsync(_baseApi);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<IssueSNOMED>>(content, _options);
        }

        public async Task<IssueSNOMED> GetIssueByIdAsync(string issueID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/{issueID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IssueSNOMED>(content, _options);
        }

        public async Task<List<IssueSNOMED>> GetAllIssuesByPatientIDAsync(int patientID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/ByPatient/{patientID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<IssueSNOMED>>(content, _options);
        }

        public async Task<List<IssueSNOMED>> GetAllIssuesByPrescriptionIDAsync(int prescriptionID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/ByPrescription/{prescriptionID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<IssueSNOMED>>(content, _options);
        }

        public async Task<IssueSNOMED> InsertIssueAsync(IssueSNOMED issue)
        {
            var json = JsonSerializer.Serialize(issue, _options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IssueSNOMED>(responseContent, _options);
        }

        public async Task<IssueSNOMED> UpdateIssueAsync(IssueSNOMED issue)
        {
            var json = JsonSerializer.Serialize(issue, _options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);  // Changed to HttpPost as per controller
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IssueSNOMED>(responseContent, _options);
        }

        public async Task DeleteIssueAsync(string issueID)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApi}/{issueID}");
            response.EnsureSuccessStatusCode();
        }
    }
}
