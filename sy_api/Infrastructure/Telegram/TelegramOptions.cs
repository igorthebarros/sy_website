namespace Infrastructure.Telegram
{
    public class TelegramOptions
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string BotToken { get; set; } = string.Empty;
        public string AllowedUserId { get; set; } = string.Empty;
        public string PhotoStoragePath { get; set; } = string.Empty;
        public string WebhookSecretToken { get; set; } = string.Empty;
    }
}
