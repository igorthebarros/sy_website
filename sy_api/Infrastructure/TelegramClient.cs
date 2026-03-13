using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public interface ITelegramClient
    {
        Task Webhook();
        Task PostAsync(string url, HttpContent content);
        Task<byte[]> GetByteArrayAsync(string url);
        Task<string> GetStringAsync(string url);
    }

    public class TelegramClient : ITelegramClient
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public TelegramClient(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task Webhook()
        {

        }

        public async Task PostAsync(string url, HttpContent content)
        {
            await _httpClient.PostAsync(url, content);
        }

        public async Task<byte[]> GetByteArrayAsync(string url)
        {
            return await _httpClient.GetByteArrayAsync(url);
        }

        public async Task<string> GetStringAsync(string url)
        {
            return await _httpClient.GetStringAsync(url);
        }
    }
}
