namespace Service.Domain.Entities
{
    public class TelegramRequest
    {
        public TelegramMesssage Message { get; set; } = new TelegramMesssage();

    }

    public class TelegramMesssage
    {
        public string MessageId { get; set; } = string.Empty;
        public string MessageChatId { get; set; } = string.Empty;
        public string MessageText { get; set; } = string.Empty;
        public IList<string> Photos { get; set; } = new List<string>();
        public TelegramDocument? Document { get; set; }
    }

    public class TelegramFileResponse
    {
        public TelegramFileResult Result { get; set; } = new TelegramFileResult();
    }

    public class TelegramFileResult
    {
        public string FilePath { get; set; } = string.Empty;
    }

    public class TelegramDocument
    {
        public string FileId { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
    }
}
