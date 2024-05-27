using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NeuroSpec.Shared.Models.DTO;

namespace NeuroSpecCompanion.Services
{
    public class OntologyTermService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;

        public OntologyTermService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.somee.com/api/OntologyTerm";
        }

        public async Task<OntologyTerm> GetOntologyTermByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}?id={id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<OntologyTerm>(content);
        }

        public async Task<OntologyTerm> InsertOntologyTermAsync(OntologyTerm ontologyTerm)
        {
            var json = JsonSerializer.Serialize(ontologyTerm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<OntologyTerm>(responseContent);
        }

        public async Task UpdateOntologyTermAsync(string id, OntologyTerm ontologyTerm)
        {
            var json = JsonSerializer.Serialize(ontologyTerm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseApi}/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteOntologyTermAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApi}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
