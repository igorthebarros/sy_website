using System.Text.Json;
using Infrastructure.Instagram;
using Infrastructure.Telegram;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Service.Domain.Entities;

namespace Service.Services
{
    public interface ITelegramService
    {
        Task Webhook(TelegramRequest update);
    }

    public class TelegramService : ITelegramService
    {
        private readonly ILogger _logger;
        private readonly TelegramClient _client;
        private readonly IConfiguration _config;
        private readonly string ALLOWED_USER_ID;
        private readonly string STORAGE_PATH;
        private readonly string TOKEN;
        private readonly string BASE_URL;
        private readonly string FILE_URL = "https://api.telegram.org/file/bot{0}/{1}";
        private readonly string GET_FILE_INFO_URL = "https://api.telegram.org/bot{0}/getFile?file_id={1}";
        private readonly string SEND_MESSAGE_URL = "https://api.telegram.org/bot{0}/sendMessage";

        public TelegramService(ILogger<TelegramService> logger, TelegramClient client, IConfiguration config   )
        {
            _logger = logger;
            _client = client;
            _config = config;

            BASE_URL = _config["Telegram:BaseUrl"] ?? string.Empty;
            ALLOWED_USER_ID = _config["Telegram:AllowedUserId"] ?? string.Empty;
            STORAGE_PATH = _config["Telegram:PhotoStoragePath"] ?? string.Empty;
            TOKEN = _config["Telegram:BotToken"] ?? string.Empty;
        }
        private string Format(string route, params object[] args)
            => string.Format(route, args);

        public async Task Webhook(TelegramRequest update)
        {
            var message = update.Message;

            // Handle commands
            if (!string.IsNullOrEmpty(message.MessageText))
            {
                if (message.MessageText.StartsWith("/shoot"))
                {
                    var album = message.MessageText.Replace("/shoot", "").Trim();
                    CurrentAlbum.Name = album;

                    await SendMessage(TOKEN, message.MessageChatId,
                        $"📷 Album set to: {album}");
                }
            }

            // Handle uploaded files
            if (!string.IsNullOrWhiteSpace(message.Document?.FileId))
            {
                await DownloadFile(TOKEN, message.Document.FileId,
                    message.Document.FileName,
                    STORAGE_PATH);

                await SendMessage(TOKEN, message.MessageChatId,
                    "✅ Photo uploaded successfully.");
            }

            // Handle photos
            if (message.Photos != null && message.Photos.Any())
            {
                var photo = message.Photos.Last(); // highest resolution

                var fileName = $"{Guid.NewGuid()}.jpg";

                await DownloadFile(TOKEN, photo,
                    fileName,
                    STORAGE_PATH);

                await SendMessage(TOKEN, message.MessageChatId,
                    "📸 Photo saved.");
            }
        }

        private async Task DownloadFile(string token, string fileId, string fileName, string storagePath)
        {
            var fileInfoUrl = Format(GET_FILE_INFO_URL, token, fileId);

            var fileInfoResponse = await _client.GetStringAsync(fileInfoUrl);

            var fileInfo = JsonSerializer.Deserialize<TelegramFileResponse>(fileInfoResponse);

            var filePath = fileInfo?.Result.FilePath;

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new InvalidOperationException("Telegram getFile response did not contain result.file_path.");
            }

            var fileUrl = Format(FILE_URL, token, filePath);

            var bytes = await _client.GetByteArrayAsync(fileUrl);

            var albumPath = Path.Combine(storagePath, CurrentAlbum.Name ?? "default");

            if (!Directory.Exists(albumPath))
                Directory.CreateDirectory(albumPath);

            var fullPath = Path.Combine(albumPath, fileName);

            await File.WriteAllBytesAsync(fullPath, bytes);
        }

        private async Task SendMessage(string token, string chatId, string text)
        {
            var url = Format(SEND_MESSAGE_URL, token);

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("chat_id", chatId),
                new KeyValuePair<string,string>("text", text)
            });

            await _client.PostAsync(url, content);
        }
    }
}
