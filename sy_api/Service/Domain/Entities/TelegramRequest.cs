using System.Text.Json.Serialization;

namespace Service.Domain.Entities
{
    // Models matching the real Telegram Bot API Update schema (snake_case).
    public class TelegramRequest
    {
        [JsonPropertyName("update_id")]
        public long UpdateId { get; set; }

        [JsonPropertyName("message")]
        public TelegramMessage? Message { get; set; }
    }

    public class TelegramMessage
    {
        [JsonPropertyName("message_id")]
        public long MessageId { get; set; }

        [JsonPropertyName("from")]
        public TelegramUser? From { get; set; }

        [JsonPropertyName("chat")]
        public TelegramChat? Chat { get; set; }

        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("photo")]
        public IList<TelegramPhotoSize>? Photo { get; set; }

        [JsonPropertyName("document")]
        public TelegramDocument? Document { get; set; }
    }

    public class TelegramUser
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }

    public class TelegramChat
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }

    public class TelegramPhotoSize
    {
        [JsonPropertyName("file_id")]
        public string FileId { get; set; } = string.Empty;

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }
    }

    public class TelegramDocument
    {
        [JsonPropertyName("file_id")]
        public string FileId { get; set; } = string.Empty;

        [JsonPropertyName("file_name")]
        public string FileName { get; set; } = string.Empty;
    }

    public class TelegramFileResponse
    {
        [JsonPropertyName("result")]
        public TelegramFileResult Result { get; set; } = new TelegramFileResult();
    }

    public class TelegramFileResult
    {
        [JsonPropertyName("file_path")]
        public string FilePath { get; set; } = string.Empty;
    }
}
