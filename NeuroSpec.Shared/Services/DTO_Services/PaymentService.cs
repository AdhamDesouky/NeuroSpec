using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NeuroSpec.Shared.Models.DTO;

namespace NeuroSpecCompanion.Shared.Services.DTO_Services
{
    public class PaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;
        private readonly JsonSerializerOptions _options;

        public PaymentService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.runasp.net/api/Payment";
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<Payment>> GetAllPaymentsAsync()
        {
            var response = await _httpClient.GetAsync(_baseApi);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Payment>>(content, _options);
        }

        public async Task<Payment> GetPaymentByIDAsync(int paymentID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/{paymentID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Payment>(content, _options);
        }

        public async Task<List<Payment>> GetPatientPaymentsAsync(int patientID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/ByPatient/{patientID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Payment>>(content, _options);
        }

        public async Task<List<Payment>> GetDoctorPaymentsAsync(int doctorID)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/ByDoctor/{doctorID}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Payment>>(content, _options);
        }

        public async Task<Payment> InsertPaymentAsync(Payment payment)
        {
            var json = JsonSerializer.Serialize(payment, _options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Payment>(responseContent, _options);
        }

        public async Task UpdatePaymentAsync(int paymentID, Payment payment)
        {
            var json = JsonSerializer.Serialize(payment, _options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseApi}/{paymentID}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePaymentAsync(int paymentID)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApi}/{paymentID}");
            response.EnsureSuccessStatusCode();
        }
    }
}
