using NeuroSpec.Shared.Models.DTO;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NeuroSpecCompanion.Shared.Services.DTO_Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;
        private readonly JsonSerializerOptions options;

        public UserService()
        {
            _httpClient = new HttpClient();
            _baseApi = "http://neurospec.somee.com/api/User";
            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var response = await _httpClient.GetAsync(_baseApi);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<User>>(content, options);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(content, options);
        }

        public async Task<List<User>> GetAllEmployeesAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/GetAllEmployees");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<User>>(content, options);
        }

        public async Task<List<User>> GetAllDoctorsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/GetAllDoctors");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<User>>(content, options);
        }

        public async Task<List<User>> GetAllAdminsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/GetAllAdmins");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<User>>(content, options);
        }

        public async Task<User> GetUserByNIDAsync(string nid)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}/GetUserByNID/{nid}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(content, options);
        }

        public async Task<User> InsertUserAsync(User user)
        {
            var json = JsonSerializer.Serialize(user, options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseApi, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(responseContent, options);
        }

        public async Task DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApi}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
