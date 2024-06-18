using NeuroSpec.Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NeuroSpecCompanion.Shared.Services.DTO_Services
{
    public class AttendanceRecordService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;
        private readonly JsonSerializerOptions options;

        public AttendanceRecordService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.runasp.net/api/attendancerecord";
            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<AttendanceRecord>> GetAllAttendanceRecordsAsync()
        {
            var response = await _httpClient.GetAsync(_baseApi);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<AttendanceRecord>>(content,options);
        }

        public async Task<AttendanceRecord> GetAttendanceRecordByIDAsync(int recordID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/{recordID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AttendanceRecord>(content, options);
        }

        public async Task<List<AttendanceRecord>> GetAttendanceRecordsByDateAsync(DateTime date)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/date/{date:yyyy-MM-dd}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<AttendanceRecord>>(content, options);
        }

        public async Task<List<AttendanceRecord>> GetUserAttendanceRecordsAsync(int userID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/user/{userID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<AttendanceRecord>>(content, options);
        }

        public async Task<AttendanceRecord> InsertAttendanceRecordAsync(AttendanceRecord attendanceRecord)
        {
            var json = JsonSerializer.Serialize(attendanceRecord, options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AttendanceRecord>(responseContent, options);
        }

        public async Task UpdateAttendanceRecordAsync(int recordID, AttendanceRecord attendanceRecord)
        {
            var json = JsonSerializer.Serialize(attendanceRecord, options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseApi}/{recordID}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAttendanceRecordAsync(int recordID)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApi}/{recordID}");
            response.EnsureSuccessStatusCode();
        }
    }
}
