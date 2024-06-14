using NeuroSpec.Shared.Models.DTO;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NeuroSpecCompanion.Shared.Services.DTO_Services
{
    public class IssueExerciseService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;

        public IssueExerciseService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.somee.com/api/IssueExercise";
        }

        public async Task<IEnumerable<IssueExercise>> GetAllIssueExercisesAsync()
        {
            var response = await _httpClient.GetAsync(_baseApi);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<IssueExercise>>(content);
        }

        public async Task<IssueExercise> GetIssueExerciseByIdAsync(int issueID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/{issueID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IssueExercise>(content);
        }

        public async Task<IEnumerable<IssueExercise>> GetAllIssueExercisesByPatientIDAsync(int patientID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/ByPatient/{patientID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<IssueExercise>>(content);
        }

        public async Task<IEnumerable<IssueExercise>> GetAllIssueExercisesByPrescriptionIDAsync(int prescriptionID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/ByPrescription/{prescriptionID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<IssueExercise>>(content);
        }

        public async Task<IssueExercise> InsertIssueExerciseAsync(IssueExercise IssueExercise)
        {
            var json = JsonSerializer.Serialize(IssueExercise);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IssueExercise>(responseContent);
        }

        public async Task UpdateIssueExerciseAsync(int issueID, IssueExercise IssueExercise)
        {
            var json = JsonSerializer.Serialize(IssueExercise);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseApi}/{issueID}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteIssueExerciseAsync(int issueID)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApi}/{issueID}");
            response.EnsureSuccessStatusCode();
        }
    }
}
