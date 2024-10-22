﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NeuroSpec.Shared.Models.DTO;

namespace NeuroSpecCompanion.Shared.Services.DTO_Services
{
    public class EvaluationTestService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;
        private readonly JsonSerializerOptions options;
        public EvaluationTestService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.runasp.net/api/EvaluationTest";
            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<EvaluationTest>> GetAllTestsAsync()
        {
            var response = await _httpClient.GetAsync(_baseApi);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<EvaluationTest>>(content, options);
        }

        public async Task<EvaluationTest> GetTestByIdAsync(int testId)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/{testId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<EvaluationTest>(content, options);
        }

        public async Task<EvaluationTest> InsertTestAsync(EvaluationTest test)
        {
            var json = JsonSerializer.Serialize(test, options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<EvaluationTest>(responseContent, options);
        }

        public async Task UpdateTestAsync(int testId, EvaluationTest test)
        {
            var json = JsonSerializer.Serialize(test, options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseApi}/{testId}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTestAsync(int testId)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApi}/{testId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
