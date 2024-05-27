using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NeuroSpec.Shared.Models.DTO;

namespace NeuroSpecCompanion.Services
{
    public class AttendanceRecordService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;

        public AttendanceRecordService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.somee.com/api/AttendanceRecord";
        }

        public async Task<IEnumerable<AttendanceRecord>> GetAllAttendanceRecordsAsync()
        {
            var response = await _httpClient.GetAsync(_baseApi);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<AttendanceRecord>>(content);
        }

        public async Task<AttendanceRecord> GetAttendanceRecordByIDAsync(int recordID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/{recordID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AttendanceRecord>(content);
        }

        public async Task<IEnumerable<AttendanceRecord>> GetAttendanceRecordsByDateAsync(DateTime date)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/date/{date:yyyy-MM-dd}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<AttendanceRecord>>(content);
        }

        public async Task<IEnumerable<AttendanceRecord>> GetUserAttendanceRecordsAsync(int userID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/user/{userID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<AttendanceRecord>>(content);
        }

        public async Task<AttendanceRecord> InsertAttendanceRecordAsync(AttendanceRecord attendanceRecord)
        {
            var json = JsonSerializer.Serialize(attendanceRecord);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AttendanceRecord>(responseContent);
        }

        public async Task UpdateAttendanceRecordAsync(int recordID, AttendanceRecord attendanceRecord)
        {
            var json = JsonSerializer.Serialize(attendanceRecord);
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
