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

        public IssueDrugService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.somee.com/api/IssueDrug";
        }

        public async Task<IEnumerable<IssueDrug>> GetAllIssueDrugsAsync()
        {
            var response = await _httpClient.GetAsync(_baseApi);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<IssueDrug>>(content);
        }

        public async Task<IssueDrug> GetIssueDrugByIdAsync(int issueID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/{issueID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IssueDrug>(content);
        }

        public async Task<IEnumerable<IssueDrug>> GetAllIssueDrugsByPatientIDAsync(int patientID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/ByPatient/{patientID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<IssueDrug>>(content);
        }

        public async Task<IEnumerable<IssueDrug>> GetAllIssueDrugsByPrescriptionIDAsync(int prescriptionID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/ByPrescription/{prescriptionID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<IssueDrug>>(content);
        }

        public async Task<IssueDrug> InsertIssueDrugAsync(IssueDrug IssueDrug)
        {
            var json = JsonSerializer.Serialize(IssueDrug);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IssueDrug>(responseContent);
        }

        public async Task UpdateIssueDrugAsync(int issueID, IssueDrug IssueDrug)
        {
            var json = JsonSerializer.Serialize(IssueDrug);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseApi}/{issueID}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteIssueDrugAsync(int issueID)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApi}/{issueID}");
            response.EnsureSuccessStatusCode();
        }
    }
}
