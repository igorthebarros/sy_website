using System.Net.Http.Headers;
using Microsoft.Extensions.Options;

namespace Infrastructure.Telegram
{
    public interface ITelegramClient
    {
        Task PostAsync(string url, HttpContent content);
        Task<byte[]> GetByteArrayAsync(string url);
        Task<string> GetStringAsync(string url);
    }

    public class TelegramClient : ITelegramClient
    {
        private readonly HttpClient _httpClient;
        private readonly TelegramOptions _options;

        public TelegramClient(HttpClient http, IOptions<TelegramOptions> options)
        {
            _httpClient = http;
            _options = options.Value;

            _httpClient.BaseAddress = new Uri(_options.BaseUrl);

            // TODO: Check the difference without it
            _httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
