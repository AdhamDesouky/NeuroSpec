using NeuroSpec.Shared.Models.DTO;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NeuroSpecCompanion.Shared.Services.DTO_Services
{
    public class IssueDrugService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;
        private readonly JsonSerializerOptions _options;

        public IssueDrugService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.runasp.net/api/IssueDrug";
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<IssueDrug>> GetAllIssueDrugsAsync()
        {
            var response = await _httpClient.GetAsync(_baseApi);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<IssueDrug>>(content, _options);
        }

        public async Task<IssueDrug> GetIssueDrugByIdAsync(string issueID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/{issueID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IssueDrug>(content, _options);
        }

        public async Task<List<IssueDrug>> GetAllIssueDrugsByPatientIDAsync(int patientID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/ByPatient/{patientID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<IssueDrug>>(content, _options);
        }

        public async Task<List<IssueDrug>> GetAllIssueDrugsByPrescriptionIDAsync(int prescriptionID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/ByPrescription/{prescriptionID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<IssueDrug>>(content, _options);
        }

        public async Task<IssueDrug> InsertIssueDrugAsync(IssueDrug issueDrug)
        {
            var json = JsonSerializer.Serialize(issueDrug, _options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IssueDrug>(responseContent, _options);
        }

        public async Task UpdateIssueDrugAsync(string issueID, IssueDrug issueDrug)
        {
            var json = JsonSerializer.Serialize(issueDrug, _options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseApi}/{issueID}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteIssueDrugAsync(string issueID)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApi}/{issueID}");
            response.EnsureSuccessStatusCode();
        }
    }
}
