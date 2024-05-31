using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NeuroSpec.Shared.Models.DTO;

namespace NeuroSpecCompanion.Shared.Services.DTO_Services
{
    public class ScanTestService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;

        public ScanTestService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.somee.com/api/ScanTest";
        }

        public async Task<IEnumerable<ScanTest>> GetAllScanTestsAsync()
        {
            var response = await _httpClient.GetAsync(_baseApi);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<ScanTest>>(content);
        }

        public async Task<ScanTest> GetScanTestByIdAsync(int scanTestID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/{scanTestID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ScanTest>(content);
        }

        public async Task<ScanTest> InsertScanTestAsync(ScanTest scanTest)
        {
            var json = JsonSerializer.Serialize(scanTest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ScanTest>(responseContent);
        }

        public async Task UpdateScanTestAsync(int scanTestID, ScanTest scanTest)
        {
            var json = JsonSerializer.Serialize(scanTest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseApi}/{scanTestID}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteScanTestAsync(int scanTestID)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApi}/{scanTestID}");
            response.EnsureSuccessStatusCode();
        }
    }
}
