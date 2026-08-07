using Service.Services;

namespace Tests;

public class CurrentAlbumStoreTests
{
    [Theory]
    [InlineData("wedding-2026", "wedding-2026")]
    [InlineData("../private", "-private")]
    [InlineData("family/photos", "family-photos")]
    [InlineData("   ", "default")]
    public void SetAlbum_SanitizesAlbumNames(string albumName, string expectedAlbumName)
    {
        var store = new CurrentAlbumStore();

        var sanitizedAlbumName = store.SetAlbum("chat-1", albumName);

        Assert.Equal(expectedAlbumName, sanitizedAlbumName);
        Assert.Equal(expectedAlbumName, store.GetAlbumOrDefault("chat-1"));
    }
}