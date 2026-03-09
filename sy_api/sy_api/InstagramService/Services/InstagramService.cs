using InstagramInfrastructure;

namespace MetaService.Services
{
    public interface IInstagramService
    {
        Task<string> GetProfileBasicAsync(string accountId);
        Task<string> GetAccountIdAsync();
        Task<string> GetAccountAsync();
        Task<string> GetProfileStatsInfoAsync();
        Task<string> GetProfileBusinessInfoAsync();
        Task<string> GetPostsAsync();
        Task<string> GetPostByIdAsync(string postId);
        Task<string> GetAccountInsightsAsync(string postId);
        Task<string> CommentAsync(string postId, string message);
        Task<string> UploadPostAsync(string imageUrl, string caption);
        Task<string> PublishPostAsync(string creationId);
    }
    public class InstagramService : IInstagramService
    {
        private readonly InstagramClient _client;
        public InstagramService(InstagramClient client)
        {
            _client = client;
        }

        public async Task<string> PublishPostAsync(string creationId)
        {
            return await _client.PublishPostAsync(creationId);
        }

        public async Task<string> UploadPostAsync(string imageUrl, string caption)
        {
            return await _client.UploadPostAsync(imageUrl, caption);
        }

        public async Task<string> CommentAsync(string mediaId, string message)
        {
            return await _client.CommentAsync(mediaId, message);
        }

        public async Task<string> GetAccountAsync()
        {
            return await _client.GetAccountAsync();
        }

        public async Task<string> GetAccountIdAsync()
        {
            return await _client.GetAccountIdAsync();
        }

        public async    Task<string> GetAccountInsightsAsync(string postId)
        {
            return await _client.GetAccountInsightsAsync(postId);
        }

        public async Task<string> GetPostByIdAsync(string postId)
        {
            return await _client.GetPostByIdAsync(postId);
        }

        public async Task<string> GetPostsAsync()
        {
            return await _client.GetPostsAsync();
        }

        public async Task<string> GetProfileBasicAsync(string accountId)
        {
            return await _client.GetProfileBasicInfoAsync();
        }

        public async Task<string> GetProfileBusinessInfoAsync()
        {
            return await _client.GetProfileBusinessInfoAsync();
        }

        public async Task<string> GetProfileStatsInfoAsync()
        {
            return await _client.GetProfileStatsInfoAsync();
        }
    }
}
