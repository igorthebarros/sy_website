using System.Text.Json;
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
        private readonly ICurrentAlbumStore _albumStore;
        private readonly IConfiguration _config;
        private readonly string ALLOWED_USER_ID;
        private readonly string STORAGE_PATH;
        private readonly string TOKEN;
        private readonly string FILE_URL = "https://api.telegram.org/file/bot{0}/{1}";
        private readonly string GET_FILE_INFO_URL = "https://api.telegram.org/bot{0}/getFile?file_id={1}";
        private readonly string SEND_MESSAGE_URL = "https://api.telegram.org/bot{0}/sendMessage";

        public TelegramService(
            ILogger<TelegramService> logger,
            TelegramClient client,
            ICurrentAlbumStore albumStore,
            IConfiguration config)
        {
            _logger = logger;
            _client = client;
            _albumStore = albumStore;
            _config = config;

            ALLOWED_USER_ID = _config["Telegram:AllowedUserId"] ?? string.Empty;
            TOKEN = _config["Telegram:BotToken"] ?? string.Empty;

            var configuredStoragePath = _config["Telegram:PhotoStoragePath"];
            STORAGE_PATH = string.IsNullOrWhiteSpace(configuredStoragePath)
                ? Path.Combine(AppContext.BaseDirectory, "photo-storage")
                : configuredStoragePath;
        }
        private string Format(string route, params object[] args)
            => string.Format(route, args);

        public async Task Webhook(TelegramRequest update)
        {
            var message = update.Message;

            if (message is null)
            {
                _logger.LogInformation(
                    "Ignoring Telegram update {UpdateId} without a message payload",
                    update.UpdateId);
                return;
            }

            var fromId = message.From?.Id.ToString() ?? string.Empty;
            var chatId = message.Chat?.Id.ToString() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(ALLOWED_USER_ID) ||
                fromId != ALLOWED_USER_ID)
            {
                _logger.LogWarning(
                    "Ignoring Telegram update from unauthorized user id {FromId}",
                    fromId);
                return;
            }

            if (string.IsNullOrWhiteSpace(chatId))
            {
                _logger.LogWarning(
                    "Ignoring Telegram update {UpdateId} without a chat id",
                    update.UpdateId);
                return;
            }

            // Handle commands
            if (!string.IsNullOrEmpty(message.Text) &&
                message.Text.StartsWith("/shoot"))
            {
                var album = message.Text.Replace("/shoot", "").Trim();
                var sanitizedAlbum = _albumStore.SetAlbum(chatId, album);

                _logger.LogInformation(
                    "Set active album for chat {ChatId} to {Album}", chatId, sanitizedAlbum);

                await SendMessage(TOKEN, chatId,
                    $"📷 Album set to: {sanitizedAlbum}");
            }

            // Handle uploaded files
            var document = message.Document;
            if (!string.IsNullOrWhiteSpace(document?.FileId))
            {
                var savedDocument = false;
                try
                {
                    var safeFileName = SanitizeFileName(document.FileName);

                    await DownloadFile(TOKEN, document.FileId,
                        safeFileName,
                        chatId,
                        STORAGE_PATH);

                    savedDocument = true;
                }
                catch (Exception e)
                {
                    _logger.LogError(e,
                        "Failed to download Telegram document {FileId} for chat {ChatId}",
                        document.FileId, chatId);
                }

                try
                {
                    var reply = savedDocument
                        ? "✅ Photo uploaded successfully."
                        : "⚠️ Failed to save the uploaded file.";

                    await SendMessage(TOKEN, chatId, reply);
                }
                catch (Exception e)
                {
                    _logger.LogError(e,
                        "Failed to send Telegram reply for document {FileId} to chat {ChatId}",
                        document.FileId, chatId);
                }
            }

            // Handle photos
            if (message.Photo != null && message.Photo.Any())
            {
                var photo = message.Photo.Last(); // highest resolution

                var savedPhoto = false;
                try
                {
                    var fileName = $"{Guid.NewGuid()}.jpg";

                    await DownloadFile(TOKEN, photo.FileId,
                        fileName,
                        chatId,
                        STORAGE_PATH);

                    savedPhoto = true;
                }
                catch (Exception e)
                {
                    _logger.LogError(e,
                        "Failed to download Telegram photo {FileId} for chat {ChatId}",
                        photo.FileId, chatId);
                }

                try
                {
                    var reply = savedPhoto
                        ? "📸 Photo saved."
                        : "⚠️ Failed to save the photo.";

                    await SendMessage(TOKEN, chatId, reply);
                }
                catch (Exception e)
                {
                    _logger.LogError(e,
                        "Failed to send Telegram reply for photo {FileId} to chat {ChatId}",
                        photo.FileId, chatId);
                }
            }
        }

        private static string SanitizeFileName(string fileName)
        {
            var safeFileName = Path.GetFileName(fileName);

            if (string.IsNullOrWhiteSpace(safeFileName))
            {
                return $"{Guid.NewGuid()}.bin";
            }

            var invalidCharacters = Path.GetInvalidFileNameChars();
            var sanitizedCharacters = safeFileName
                .Select(character => invalidCharacters.Contains(character) ? '-' : character)
                .ToArray();

            return new string(sanitizedCharacters);
        }

        private async Task DownloadFile(string token, string fileId, string fileName, string chatId, string storagePath)
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

            var albumPath = Path.Combine(storagePath, _albumStore.GetAlbumOrDefault(chatId));

            if (!Directory.Exists(albumPath))
                Directory.CreateDirectory(albumPath);

            var fullPath = Path.Combine(albumPath, fileName);

            await File.WriteAllBytesAsync(fullPath, bytes);

            _logger.LogInformation(
                "Saved Telegram file {FileName} to album path {AlbumPath}", fileName, albumPath);
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
