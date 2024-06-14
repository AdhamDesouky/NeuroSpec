using NeuroSpec.Shared.Models.DTO;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NeuroSpecCompanion.Shared.Services.DTO_Services
{
    public class IssueTestService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;

        public IssueTestService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.somee.com/api/IssueTest";
        }

        public async Task<IEnumerable<IssueTest>> GetAllIssueTestsAsync()
        {
            var response = await _httpClient.GetAsync(_baseApi);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<IssueTest>>(content);
        }

        public async Task<IssueTest> GetIssueTestByIdAsync(int issueID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/{issueID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IssueTest>(content);
        }

        public async Task<IEnumerable<IssueTest>> GetAllIssueTestsByPatientIDAsync(int patientID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/ByPatient/{patientID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<IssueTest>>(content);
        }

        public async Task<IEnumerable<IssueTest>> GetAllIssueTestsByPrescriptionIDAsync(int prescriptionID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/ByPrescription/{prescriptionID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<IssueTest>>(content);
        }

        public async Task<IssueTest> InsertIssueTestAsync(IssueTest IssueTest)
        {
            var json = JsonSerializer.Serialize(IssueTest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IssueTest>(responseContent);
        }

        public async Task UpdateIssueTestAsync(int issueID, IssueTest IssueTest)
        {
            var json = JsonSerializer.Serialize(IssueTest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseApi}/{issueID}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteIssueTestAsync(int issueID)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApi}/{issueID}");
            response.EnsureSuccessStatusCode();
        }
    }
}
