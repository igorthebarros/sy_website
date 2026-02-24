using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;

namespace MetaService.Services
{
    public interface IInstagramService
    {
        Task<string> GetPostsAsync();
        Task<string> CommentAsync(string mediaId, string message);
        Task DeleteCommentAsync(string commentId);
    }
    public class InstagramService : IInstagramService
    {
        private readonly MetaClient _client;
        public InstagramService(MetaClient client)
        {
            _client = client;
        }


    }
}
