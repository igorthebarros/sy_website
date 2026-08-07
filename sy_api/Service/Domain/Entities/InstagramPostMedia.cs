namespace MetaService.Domain.Entities
{
    public class InstagramPostMedia
    {
        public required string InstagramUserId { get; init; }
        public required string InstagramUserToken { get; init; }
        public string URL { get; set; } = string.Empty;
        public string Caption { get; set; } = string.Empty;
    }
}
