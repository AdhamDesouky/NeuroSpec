using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NeuroSpec.Shared.Models.DTO;

namespace NeuroSpecCompanion.Services
{
    public class PatientService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;
        
        public PatientService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.somee.com/api/Patient";
        }
        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            var response = await _httpClient.GetAsync(_baseApi);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Patient>>(content);
        }
        
        public async Task<Patient> GetPatientByIdAsync(string patientID)
        {
            var response = await _httpClient.GetAsync(_baseApi + "/" + patientID);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Patient>(content);
        }
        public async Task<bool> VerifyPatientAsync(string patientID, string password)
        {
            var response = await _httpClient.GetAsync(_baseApi + "/" + patientID + "/" + password);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<bool>(content);
        }
        public async Task<Patient> InsertPatientAsync(Patient patient)
        {
            var json = JsonSerializer.Serialize(patient);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Patient>(responseContent);
        }
        public async Task UpdatePatientAsync(string patientID, Patient patient)
        {
            var json = JsonSerializer.Serialize(patient);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_baseApi + "/" + patientID, content);
            response.EnsureSuccessStatusCode();
        }
        public async Task DeletePatientAsync(string patientID)
        {
            var response = await _httpClient.DeleteAsync(_baseApi + "/" + patientID);
            response.EnsureSuccessStatusCode();
        }
    }
}
