using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Service.Domain.Entities;
using System.Text.Json;

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

        public async Task Webhook(TelegramRequest update)
        {
            var message = update.Message;

            // Handle commands
            if (!string.IsNullOrEmpty(message.Text))
            {
                if (message.Text.StartsWith("/shoot"))
                {
                    var album = message.Text.Replace("/shoot", "").Trim();
                    CurrentAlbum.Name = album;

                    await SendMessage(TOKEN, message.Chat.Id,
                        $"📷 Album set to: {album}");
                }
            }

            // Handle uploaded files
            if (message.Document != null)
            {
                await DownloadFile(TOKEN, message.Document.FileId,
                    message.Document.FileName,
                    STORAGE_PATH);

                await SendMessage(TOKEN, message.Chat.Id,
                    "✅ Photo uploaded successfully.");
            }

            // Handle photos
            if (message.Photo != null && message.Photo.Any())
            {
                var photo = message.Photo.Last(); // highest resolution

                var fileName = $"{Guid.NewGuid()}.jpg";

                await DownloadFile(TOKEN, photo.FileId,
                    fileName,
                    STORAGE_PATH);

                await SendMessage(TOKEN, message.Chat.Id,
                    "📸 Photo saved.");
            }

            await _client.Webhook();
        }

        private async Task DownloadFile(string token, string fileId, string fileName, string storagePath)
        {
            var fileInfoUrl = $"https://api.telegram.org/bot{token}/getFile?file_id={fileId}";
            var fileInfoResponse = await _client.GetStringAsync(fileInfoUrl);

            var fileInfo = JsonSerializer.Deserialize<TelegramFileResponse>(fileInfoResponse);

            var filePath = fileInfo!.Result.FilePath;

            var fileUrl = $"https://api.telegram.org/file/bot{token}/{filePath}";

            var bytes = await _client.GetByteArrayAsync(fileUrl);

            var albumPath = Path.Combine(storagePath, CurrentAlbum.Name ?? "default");

            if (!Directory.Exists(albumPath))
                Directory.CreateDirectory(albumPath);

            var fullPath = Path.Combine(albumPath, fileName);

            await File.WriteAllBytesAsync(fullPath, bytes);
        }

        private async Task SendMessage(string token, long chatId, string text)
        {
            var url = $"https://api.telegram.org/bot{token}/sendMessage";

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("chat_id", chatId.ToString()),
                new KeyValuePair<string,string>("text", text)
            });

            await _client.PostAsync(url, content);
        }
    }
}
