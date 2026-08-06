using System.Collections.Concurrent;

namespace Service.Services
{
    public interface ICurrentAlbumStore
    {
        void SetAlbum(string chatId, string albumName);
        string GetAlbumOrDefault(string chatId);
    }

    public class CurrentAlbumStore : ICurrentAlbumStore
    {
        private readonly ConcurrentDictionary<string, string> _albumsByChat = new();

        public void SetAlbum(string chatId, string albumName)
        {
            _albumsByChat[chatId] = albumName;
        }

        public string GetAlbumOrDefault(string chatId)
        {
            return _albumsByChat.TryGetValue(chatId, out var albumName) &&
                   !string.IsNullOrWhiteSpace(albumName)
                ? albumName
                : "default";
        }
    }
}