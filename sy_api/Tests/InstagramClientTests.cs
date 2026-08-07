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
        Assert.Equal("https://graph.instagram.com/demo-account/media?fields=id,caption,media_url,media_type,permalink&access_token=demo-token", handler.LastRequestUri?.ToString());
    }

    [Fact]
    public async Task GetProfileBasicInfoAsync_UsesRouteAccountId()
    {
        var handler = new StubHttpMessageHandler(new HttpResponseMessage
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = new StringContent("{}")
        });

        var instagramClient = CreateClient(handler);

        await instagramClient.GetProfileBasicInfoAsync("route-account");

        Assert.Equal("https://graph.instagram.com/route-account?fields=id,username,name,biography,profile_picture_url,website&access_token=demo-token", handler.LastRequestUri?.ToString());
    }

    [Fact]
    public async Task GetAccountInsightsAsync_UsesRouteAccountId()
    {
        var handler = new StubHttpMessageHandler(new HttpResponseMessage
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = new StringContent("{}")
        });

        var instagramClient = CreateClient(handler);

        await instagramClient.GetAccountInsightsAsync("insights-account");

        Assert.Equal("https://graph.instagram.com/insights-account/insights&access_token=demo-token", handler.LastRequestUri?.ToString());
    }

    [Fact]
    public async Task GetPostsAsync_Throws_WhenAccountIdIsMissing()
    {
        var handler = new StubHttpMessageHandler(new HttpResponseMessage
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = new StringContent("{}")
        });

        var client = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://graph.instagram.com/")
        };

        var options = Options.Create(new InstagramOptions
        {
            BaseUrl = "https://graph.instagram.com/",
            AccountId = string.Empty,
            Token = "demo-token"
        });

        var instagramClient = new InstagramClient(client, options);

        await Assert.ThrowsAsync<InvalidOperationException>(() => instagramClient.GetPostsAsync());
    }

    private static InstagramClient CreateClient(StubHttpMessageHandler handler)
    {
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

        return new InstagramClient(client, options);
    }

    private sealed class StubHttpMessageHandler(HttpResponseMessage response) : HttpMessageHandler
    {
        public Uri? LastRequestUri { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastRequestUri = request.RequestUri;
            return Task.FromResult(response);
        }
    }
}
