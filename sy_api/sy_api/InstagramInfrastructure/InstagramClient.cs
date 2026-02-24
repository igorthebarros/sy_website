using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace InstagramInfrastructure
{
    public class InstagramClient
    {
        // TODO: Add try catch and logging
        private readonly HttpClient _httpClient;
        private readonly InstagramOptions _options;

        public InstagramClient(HttpClient http, IOptions<InstagramOptions> opts)
        {
            _httpClient = http;
            _options = opts.Value;

            _httpClient.BaseAddress = new Uri(_options.BaseUrl);

            // TODO: Check the difference without it
            _httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private string Format(string route, params object[] args)
            => string.Format(route, args);

        public async Task<string> GetAccountIdAsync()
        {
            var url = Format(
                InstagramRoutesConstant.INSTAGRAM_ACCOUNT_ID,
                _options.Token);

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetProfileBasicAsync(string accountId)
        {
            var url = Format(
                InstagramRoutesConstant.INSTAGRAM_PROFILE_BASIC_INFO,
                accountId,
                _options.Token);

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetPostByIdAsync(string postId)
        {
            var url = Format(
                InstagramRoutesConstant.INSTAGRAM_POST_BY_ID,
                postId,
                _options.Token);

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CommentAsync(string postId, string message)
        {
            var url = Format(
                  InstagramRoutesConstant.INSTAGRAM_POST_COMMENT,
                  postId);

            var content = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "message", message },
                    { "access_token", _options.Token }
                }
            );

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PublishPostAsync(string creationId)
        {
            var content = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "creation_id", creationId },
                    { "access_token", _options.Token }
                }
            );

            var response = await _httpClient.PostAsync(
                InstagramRoutesConstant.INSTAGRAM_POST_PUBLISH,
                content);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetPostsAsync()
        {
            var url = Format(
                InstagramRoutesConstant.INSTAGRAM_ACCOUNT,
                _options.Token);

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
