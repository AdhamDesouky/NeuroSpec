using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NeuroSpecCompanion.Shared.Services.DTO_Services;


namespace NeuroSpecCompanion.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;
        public AuthService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.somee.com/api/Patient";
        }

        public async Task<bool> VerifyPatientCallerAsync(int patientID, string password,bool autoLogin)
        {
            PatientService patientService = new PatientService();

            var response = await _httpClient.GetAsync(_baseApi + "/" + patientID + "/" + password);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var isValid= JsonSerializer.Deserialize<bool>(content);
            if (isValid)
            {
                var patient = await patientService.GetPatientByIdAsync(patientID);

                if (patient != null)
                {
                    LoggedInPatientService.LoggedInPatient = patient;
                    if (autoLogin)
                    {
                        // Save credentials securely for auto login
                        await SecureStorage.SetAsync("PatientID", patientID.ToString());
                        await SecureStorage.SetAsync("Password", password);
                    }

                }
            }
            return isValid;
        }
        public async Task<bool> AutoLoginAsync()
        {
            var patientIdString = await SecureStorage.GetAsync("PatientID");
            var password = await SecureStorage.GetAsync("Password");

            if (!string.IsNullOrEmpty(patientIdString) && !string.IsNullOrEmpty(password))
            {
                if (int.TryParse(patientIdString, out var patientID))
                {
                    return await VerifyPatientCallerAsync(patientID, password, true);
                }
            }

            return false;
        }
    }
}
