using System.Text.Json;
using Service.Domain.Entities;

namespace Tests;

public class TelegramRequestTests
{
    [Fact]
    public void TelegramRequest_DeserializesRealUpdateSchema()
    {
        var payload = """
        {
          "update_id": 123456789,
          "message": {
            "message_id": 42,
            "from": { "id": 111222333 },
            "chat": { "id": 111222333 },
            "text": "/shoot wedding-2026",
            "photo": [
              { "file_id": "small-file-id", "width": 90, "height": 60 },
              { "file_id": "large-file-id", "width": 1280, "height": 853 }
            ],
            "document": { "file_id": "doc-file-id", "file_name": "photo.jpg" }
          }
        }
        """;

        var update = JsonSerializer.Deserialize<TelegramRequest>(payload);

        Assert.NotNull(update);
        Assert.Equal(123456789, update.UpdateId);
        Assert.NotNull(update.Message);
        Assert.Equal(42, update.Message.MessageId);
        Assert.Equal(111222333, update.Message.From?.Id);
        Assert.Equal(111222333, update.Message.Chat?.Id);
        Assert.Equal("/shoot wedding-2026", update.Message.Text);
        Assert.Equal(2, update.Message.Photo?.Count);
        Assert.Equal("large-file-id", update.Message.Photo?.Last().FileId);
        Assert.Equal("doc-file-id", update.Message.Document?.FileId);
        Assert.Equal("photo.jpg", update.Message.Document?.FileName);
    }

    [Fact]
    public void TelegramRequest_DeserializesUpdateWithoutMessage()
    {
        var payload = """{ "update_id": 987654321 }""";

        var update = JsonSerializer.Deserialize<TelegramRequest>(payload);

        Assert.NotNull(update);
        Assert.Equal(987654321, update.UpdateId);
        Assert.Null(update.Message);
    }

    [Fact]
    public void TelegramFileResponse_DeserializesSnakeCaseFilePath()
    {
        var payload = """{ "ok": true, "result": { "file_path": "photos/file_1.jpg" } }""";

        var response = JsonSerializer.Deserialize<TelegramFileResponse>(payload);

        Assert.NotNull(response);
        Assert.Equal("photos/file_1.jpg", response.Result.FilePath);
    }
}
