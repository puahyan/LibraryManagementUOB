using Newtonsoft.Json;
using System.Text;

namespace LibraryManagementSystem.Services
{
    public class DynatraceLoggerService : IDynatraceLoggerService
    {
        private readonly HttpClient _httpClient;
        private readonly string _environmentId;
        private readonly string _apiToken;
        private readonly string _url;

        public DynatraceLoggerService(string environmentId, string apiToken)
        {
            _environmentId = environmentId;
            _apiToken = apiToken;
            _url = $"https://{_environmentId}.live.dynatrace.com/api/v2/logs/ingest";

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Api-Token {_apiToken}");
        }

        public async Task LogAsync(string message, object attributes = null)
        {
            var logEvent = new
            {
                timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                log = message,
                attributes = attributes ?? new { }
            };

            var logs = new[] { logEvent };
            var jsonPayload = JsonConvert.SerializeObject(logs);

            try
            {
                var response = await _httpClient.PostAsync(_url, new StringContent(jsonPayload, Encoding.UTF8, "application/json"));
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(response.StatusCode + responseContent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
