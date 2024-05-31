using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NeuroSpec.Shared.Models.DTO;

namespace NeuroSpecCompanion.Shared.Services.DTO_Services
{
    public class MedicalRecordService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;

        public MedicalRecordService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.somee.com/api/MedicalRecord";
        }

        public async Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordsAsync()
        {
            var response = await _httpClient.GetAsync(_baseApi);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<MedicalRecord>>(content);
        }

        public async Task<MedicalRecord> GetMedicalRecordByIDAsync(int recordID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/{recordID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<MedicalRecord>(content);
        }

        public async Task<IEnumerable<MedicalRecord>> GetAllPatientRecordsAsync(int patientID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/ByPatient/{patientID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<MedicalRecord>>(content);
        }

        public async Task<MedicalRecord> InsertMedicalRecordAsync(MedicalRecord medicalRecord)
        {
            var json = JsonSerializer.Serialize(medicalRecord);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<MedicalRecord>(responseContent);
        }

        public async Task UpdateMedicalRecordAsync(int recordID, MedicalRecord medicalRecord)
        {
            var json = JsonSerializer.Serialize(medicalRecord);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseApi}/{recordID}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteMedicalRecordAsync(int recordID)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApi}/{recordID}");
            response.EnsureSuccessStatusCode();
        }
    }
}
