using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NeuroSpec.Shared.Models.DTO;

namespace NeuroSpecCompanion.Shared.Services.DTO_Services
{
    public class EvaluationTestFeedbackService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;

        public EvaluationTestFeedbackService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.runasp.net/api/EvaluationTestFeedback";
        }

        public async Task<List<EvaluationTestFeedBack>> GetAllFeedbackAsync()
        {
            var response = await _httpClient.GetAsync(_baseApi);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<EvaluationTestFeedBack>>(content);
        }

        public async Task<EvaluationTestFeedBack> GetFeedbackByIdAsync(int feedbackId)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/{feedbackId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<EvaluationTestFeedBack>(content);
        }

        public async Task<List<EvaluationTestFeedBack>> GetFeedbackByPatientAsync(int patientId)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/ByPatient/{patientId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<EvaluationTestFeedBack>>(content);
        }

        public async Task<List<EvaluationTestFeedBack>> GetFeedbackByVisitAsync(int visitId)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/ByVisit/{visitId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<EvaluationTestFeedBack>>(content);
        }

        public async Task<EvaluationTestFeedBack> InsertFeedbackAsync(EvaluationTestFeedBack feedback)
        {
            var json = JsonSerializer.Serialize(feedback);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<EvaluationTestFeedBack>(responseContent);
        }

        public async Task DeleteFeedbackAsync(int feedbackId)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApi}/{feedbackId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAllVisitFeedbackAsync(int visitId)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApi}/ByVisit/{visitId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
