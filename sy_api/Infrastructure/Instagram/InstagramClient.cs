using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace Infrastructure.Instagram
{
    public interface IInstagramClient
    {
        Task<string> GetAccountIdAsync();
        Task<string> GetAccountAsync();
        Task<string> GetProfileBasicInfoAsync();
        Task<string> GetProfileStatsInfoAsync();
        Task<string> GetProfileBusinessInfoAsync();
        Task<string> GetPostsAsync();
        Task<string> GetPostByIdAsync(string postId);
        Task<string> GetAccountInsightsAsync(string postId);
        Task<string> CommentAsync(string postId, string message);
        Task<string> UploadPostAsync(string imageUrl, string caption);
        Task<string> PublishPostAsync(string creationId);
    }

    public class InstagramClient : IInstagramClient
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

        #region BASICS
        public async Task<string> GetAccountIdAsync()
        {
            var accountId = _options.AccountId;

            if (string.IsNullOrEmpty(accountId))
            {
                throw new InvalidOperationException("Instagram Account ID is not configured.");
            };

            // Add accountId to the URL

            var url = Format(
                InstagramRoutesConstant.INSTAGRAM_ACCOUNT_ID,
                _options.Token);

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetInstagramAccountAsync()
        {
            var url = Format(
                InstagramRoutesConstant.INSTAGRAM_ACCOUNT,
                _options.Token);

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        #endregion

        public async Task<string> GetProfileBasicInfoAsync()
        {
            var accountId = _options.AccountId;

            if (string.IsNullOrEmpty(accountId))
            {
                throw new InvalidOperationException("Instagram Account ID is not configured.");
            };

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

        public async Task<string> GetPostsAsync()
        {
            if (string.IsNullOrWhiteSpace(_options.AccountId))
            {
                throw new InvalidOperationException("Instagram Account ID is not configured.");
            }

            var path = InstagramRoutesConstant.INSTAGRAM_POSTS;
            var uri = Format(path, _options.AccountId, _options.Token);

            var response = await _httpClient.GetAsync(uri);
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

        public async Task<string> UploadPostAsync(string imageUrl, string caption)
        {
            var path = InstagramRoutesConstant.INSTAGRAM_POST_UPLOAD;

            var url = Format(path, _options.AccountId);

            var content = new FormUrlEncodedContent(new[]
            {
                //new KeyValuePair<string, string>("access_token", media.InstagramUserToken!),
                //new KeyValuePair<string, string>("image_url", media.URL),
                new KeyValuePair<string, string>("is_carousel_item", "false"),
                new KeyValuePair<string, string>("alt_text", "🤖 Testing Meta'\''s IG Graph API...bagulho doido"),
                new KeyValuePair<string, string>("caption", "🤖 Testing Meta'\''s IG Graph API...bagulho doido")
            });

            var response = await _httpClient.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetAccountAsync()
        {
            var path = InstagramRoutesConstant.INSTAGRAM_ACCOUNT;

            var uri = Format(path, _options.Token);

            var response = await _httpClient.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetProfileStatsInfoAsync()
        {
            var path = InstagramRoutesConstant.INSTAGRAM_PROFILE_STATS_INFO;

            var uri = Format(path, _options.AccountId, _options.Token);

            var response = await _httpClient.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetProfileBusinessInfoAsync()
        {
            var path = InstagramRoutesConstant.INSTAGRAM_PROFILE_BUSINESS_INFO;

            var uri = Format(path, _options.AccountId, _options.Token);

            var response = await _httpClient.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetAccountInsightsAsync(string postId)
        {
            var path = InstagramRoutesConstant.INSTAGRAM_DATA_INSIGHTS;

            var uri = Format(path, _options.AccountId, _options.Token);

            var response = await _httpClient.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
