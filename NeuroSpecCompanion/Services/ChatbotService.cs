using GenerativeAI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace NeuroSpecCompanion.Services
{



    public class ChatbotService
    {
        string apiKey = "AIzaSyAIY2li7RtySecrkx0FNMKx0MwOY4YY2iE";// for gemini
        GenerativeModel _model;
        string huggingFaceUrl = "https://onabajomonsurat-medical-diagnosis-chatbot.hf.space/run/predict";

        public ChatbotService()
        {
            _model = new GenerativeModel(apiKey);

        }

        //gemini

        //public async Task<string> ProcessMessageAsync(string text)
        //{
        //    try
        //    {
        //        var chat = _model.StartChat(new GenerativeAI.Types.StartChatParams());
        //        var response = await chat.SendMessageAsync(text);
        //        return response;

        //    }
        //    catch (Exception e)
        //    {
        //        return $"Request error: {e.Message}";
        //    }
        //}

        public async Task<string> ProcessMessageAsync(string text)
        {
            var payload = new
            {
                data = new string[] { text },
                fn_index = 1
            };

            string jsonPayload = JsonConvert.SerializeObject(payload);

            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync(huggingFaceUrl, content);

                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject jsonResponse = JObject.Parse(responseBody);

                    JArray data = (JArray)jsonResponse["data"];
                    string result = data[0].ToString();
                    return result;
                }
                catch (HttpRequestException e)
                {
                    return ("Request error: " + e.Message);
                }
            }
        }

    }

    //galal

    //public class ArabicApiResponse
    //{
    //    public string answer { get; set; }
    //}


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

