using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NeuroSpecBackend.Services
{
    public class ChatbotService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        
        public ChatbotService()
        {
            _httpClient = new HttpClient();
            _url = "https://abdelrahmansaleh.us-east-1.modelbit.com/v1/predict_weather/latest";

        }
        public async Task<string> ProcessMessageAsync(string text)
        {
            try
            {
                // JSON body
                var jsonBody = new { data = text  };
                var json = JsonSerializer.Serialize(jsonBody);

                // Set up the request content
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send the POST request
                var response = await _httpClient.PostAsync(_url, content);

                // Ensure the response is successful
                response.EnsureSuccessStatusCode();

                // Read the response content
                var responseContent = await response.Content.ReadAsStringAsync();

                // Output the response
                return responseContent;
            }
            catch (HttpRequestException e)
            {
                // Output error message
                return $"Request error: {e.Message}";
            }
        }

    }
}

