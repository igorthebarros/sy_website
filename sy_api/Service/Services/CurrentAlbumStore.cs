using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace Service.Services
{
    public interface ICurrentAlbumStore
    {
        string SetAlbum(string chatId, string albumName);
        string GetAlbumOrDefault(string chatId);
    }

    public class CurrentAlbumStore : ICurrentAlbumStore
    {
        private readonly ConcurrentDictionary<string, string> _albumsByChat = new();

        public string SetAlbum(string chatId, string albumName)
        {
            var sanitizedAlbumName = SanitizeAlbumName(albumName);
            _albumsByChat[chatId] = sanitizedAlbumName;
            return sanitizedAlbumName;
        }

        public string GetAlbumOrDefault(string chatId)
        {
            return _albumsByChat.TryGetValue(chatId, out var albumName) &&
                   !string.IsNullOrWhiteSpace(albumName)
                ? albumName
                : "default";
        }

        private static string SanitizeAlbumName(string albumName)
        {
            if (string.IsNullOrWhiteSpace(albumName))
            {
                return "default";
            }

            var invalidCharacters = Path.GetInvalidFileNameChars();
            var sanitizedCharacters = albumName
                .Trim()
                .Select(character => invalidCharacters.Contains(character) || character == Path.DirectorySeparatorChar || character == Path.AltDirectorySeparatorChar
                    ? '-'
                    : character)
                .ToArray();

            var sanitizedAlbumName = new string(sanitizedCharacters)
                .Replace("..", "-")
                .Replace("--", "-")
                .Trim(' ', '.');

            sanitizedAlbumName = Regex.Replace(sanitizedAlbumName, "-{2,}", "-");

            return string.IsNullOrWhiteSpace(sanitizedAlbumName)
                ? "default"
                : sanitizedAlbumName;
        }
    }
}