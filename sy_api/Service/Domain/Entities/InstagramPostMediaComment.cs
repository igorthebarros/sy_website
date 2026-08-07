namespace MetaService.Domain.Entities
{
    public class InstagramPostMediaComment
    {
        public required string PostId { get; init; }
        public string CommentMessage { get; set; } = string.Empty;
    }
}
