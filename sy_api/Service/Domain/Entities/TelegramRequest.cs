namespace Service.Domain.Entities
{
    public static class CurrentAlbum
    {
        public static string? Name { get; set; }
    }

    public class TelegramRequest
    {
        public TelegramMesssage Message { get; set; } = new TelegramMesssage();

    }

    public class TelegramMesssage
    {
        public long Message_Id { get; set; }
        public TelegramUser From { get; set; } = new TelegramUser();
        public TelegramChat Chat { get; set; } = new TelegramChat();
        public string Text { get; set; } = string.Empty;
        public TelegramDocument Document { get; set; } = new TelegramDocument();
        public List<TelegramPhoto> Photo { get; set; } = new List<TelegramPhoto>();
    }

    public class TelegramUser
    {
        public long Id { get; set; }
    }

    public class TelegramChat
    {
        public long Id { get; set; }
    }

    public class TelegramDocument
    {
        public string FileId { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
    }

    public class TelegramPhoto
    {
        public string FileId { get; set; } = string.Empty;
    }

    public class TelegramFileResponse
    {
        public TelegramFileResult Result { get; set; } = new TelegramFileResult();
    }

    public class TelegramFileResult
    {
        public string FilePath { get; set; } = string.Empty;
    }
}
