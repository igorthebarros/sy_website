using Infrastructure.Instagram;
using Microsoft.Extensions.Options;

namespace Tests;

public class InstagramClientTests
{
    [Fact]
    public async Task GetPostsAsync_ReturnsResponseBody_WhenApiRespondsSuccessfully()
    {
        var handler = new StubHttpMessageHandler(new HttpResponseMessage
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = new StringContent("{\"data\":[{\"id\":\"1\"}]}")
        });

        var client = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://graph.instagram.com/")
        };

        var options = Options.Create(new InstagramOptions
        {
            BaseUrl = "https://graph.instagram.com/",
            AccountId = "demo-account",
            Token = "demo-token"
        });

        var instagramClient = new InstagramClient(client, options);

        var result = await instagramClient.GetPostsAsync();

        Assert.Contains("\"data\"", result);
        Assert.Contains("\"id\"", result);
    }

    private sealed class StubHttpMessageHandler(HttpResponseMessage response) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            => Task.FromResult(response);
    }
}
