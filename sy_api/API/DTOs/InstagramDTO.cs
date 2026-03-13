namespace InstagramAPI.DTOs
{
    public class InstagramDTO
    {
        public string PostId { get; set; } = string.Empty;
        public string CommentMessage { get; set; } = string.Empty;
    }

    public class PostMediaDTO
    {
        public string InstagramUserId { get; set; } = string.Empty;
        public string InstagramUserToken { get; set; } = string.Empty;
        public string URL { get; set; } = string.Empty;
        public string Caption { get; set; } = string.Empty;
    }
}
