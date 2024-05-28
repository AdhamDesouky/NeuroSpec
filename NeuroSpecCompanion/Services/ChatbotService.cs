using GenerativeAI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NeuroSpecCompanion.Services
{

    public class ArabicApiResponse
    {
        public string answer { get; set; }
    }


    public class ChatbotService
    {
        string apiKey = "AIzaSyC96km5Mts6OPAfkk27qsZmCmrlHnnCCxw";
        GenerativeModel _model;
        public ChatbotService()
        {
            _model = new GenerativeModel(apiKey);
        }

        public async Task<string> ProcessMessageAsync(string text)
        {
            try
            {
                var chat = _model.StartChat(new GenerativeAI.Types.StartChatParams());
                var response = await chat.SendMessageAsync(text);
                return response;
            }
            catch (Exception e)
            {
                return $"Request error: {e.Message}";
            }
        }
    }

    //galal
    //public class ChatbotService
    //{
    //    private readonly HttpClient _httpClient;
    //    private readonly string _url;

    //    public ChatbotService()
    //    {
    //        _httpClient = new HttpClient();
    //        _url = "https://cfae-154-178-134-151.ngrok-free.app/predict";

    //    }
    //    public async Task<string> ProcessMessageAsync(string text)
    //    {
    //        try
    //        {

    //            // JSON body
    //            var jsonBody = new { prompt = text };
    //            var json = JsonSerializer.Serialize(jsonBody);

    //            // Set up the request content
    //            var content = new StringContent(json, Encoding.UTF8, "application/json");

    //            // Send the POST request
    //            var response = await _httpClient.PostAsync(_url, content);

    //            response.EnsureSuccessStatusCode();

    //            var jsonString = await response.Content.ReadAsStringAsync();
    //            var options = new JsonSerializerOptions
    //            {
    //                PropertyNameCaseInsensitive = true,
    //                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    //            };
    //            var arabicResponse = JsonSerializer.Deserialize<ArabicApiResponse>(jsonString, options);

    //            return arabicResponse.answer;
    //        }
    //        catch (HttpRequestException e)
    //        {
    //            // Output error message
    //            return $"Request error: {e.Message}";
    //        }
    //    }

    //}
}

