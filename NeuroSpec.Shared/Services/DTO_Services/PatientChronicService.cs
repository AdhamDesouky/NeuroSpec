﻿using NeuroSpec.Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NeuroSpec.Shared.Services.DTO_Services
{
    public class PatientChronicService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;
        public PatientChronicService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.somee.com/api/PatientChronic";
        }

        public async Task<List<PatientChronic>> GetAllPatientChronicsAsync()
        {
            var response = await _httpClient.GetAsync(_baseApi);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<PatientChronic>>(content);
        }

        public async Task<PatientChronic> GetPatientChronicByIDAsync(int chronicID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/{chronicID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PatientChronic>(content);
        }

        public async Task<List<PatientChronic>> GetPatientChronicsByPatientIDAsync(int patientID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/patient/{patientID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<PatientChronic>>(content);
        }

        public async Task<PatientChronic> InsertPatientChronicAsync(PatientChronic patientChronic)
        {
            var json = JsonSerializer.Serialize(patientChronic);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PatientChronic>(responseContent);
        }
        public async Task<PatientChronic> UpdatePatientChronicAsync(PatientChronic patientChronic)
        {
            var json = JsonSerializer.Serialize(patientChronic);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PatientChronic>(responseContent);
        }
        public async Task DeletePatientChronicAsync(int chronicID)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApi}/{chronicID}");
            response.EnsureSuccessStatusCode();
        }

    }
}