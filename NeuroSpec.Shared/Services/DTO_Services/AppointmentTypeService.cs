﻿using NeuroSpec.Shared.Models.DTO;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NeuroSpecCompanion.Shared.Services.DTO_Services
{
    public class AppointmentTypeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;
        private readonly JsonSerializerOptions options;

        public AppointmentTypeService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.runasp.net/api/AppointmentType";
            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<AppointmentType>> GetAllAppointmentTypesAsync()
        {
            var response = await _httpClient.GetAsync(_baseApi);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<AppointmentType>>(content,options);
        }

        public async Task<AppointmentType> GetAppointmentTypeByIDAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AppointmentType>(content,options);
        }

        public async Task<AppointmentType> InsertAppointmentTypeAsync(AppointmentType appointmentType)
        {
            var json = JsonSerializer.Serialize(appointmentType);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AppointmentType>(responseContent,options);
        }

        public async Task UpdateAppointmentTypeAsync(int id, AppointmentType appointmentType)
        {
            var json = JsonSerializer.Serialize(appointmentType);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseApi}/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAppointmentTypeAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApi}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
