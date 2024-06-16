using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NeuroSpec.Shared.Models.DTO;


namespace NeuroSpecCompanion.Shared.Services.DTO_Services
{
    public class PatientService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;
        private readonly JsonSerializerOptions options;
        public PatientService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.somee.com/api/Patient";
            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }
        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            var response = await _httpClient.GetAsync(_baseApi);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Patient>>(content,options);
        }
        
        public async Task<Patient> GetPatientByIdAsync(int patientID)
        {
            var response = await _httpClient.GetAsync(_baseApi + "/" + patientID);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Patient>(content,options);
        }

        public async Task<Hl7.Fhir.Model.Patient> GetFHIRPatientByIdAsync(int patientID)
        {
            var response = await _httpClient.GetAsync(_baseApi + "/onFHIR/" + patientID);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Hl7.Fhir.Model.Patient>(content, options);
        }

        public async Task<bool> VerifyPatientAsync(int patientID, string password)
        {
            var response = await _httpClient.GetAsync(_baseApi + "/" + patientID + "/" + password);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<bool>(content,options);

        }
       
        //update patient
        public async Task<Patient> UpdatePatientAsync(Patient patient)
        {
            var json = JsonSerializer.Serialize(patient, options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_baseApi + "/" + patient.PatientID, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Patient>(responseContent,options);
        }

        

        public async Task<Patient> InsertPatientAsync(Patient patient)
        {
            var json = JsonSerializer.Serialize(patient, options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Patient>(responseContent, options);
        }

        //        [HttpGet("doctor/{doctorID}")]

        public async Task<List<Patient>> GetPatientsByDoctorAsync(int doctorID)
        {
            var response = await _httpClient.GetAsync(_baseApi + "/doctor/" + doctorID);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Patient>>(content, options);
        }

    }
}
