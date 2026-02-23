using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace MetaAPI.Controllers
{
    public class MetaController : ControllerBase
    {
        private readonly IConfiguration _config;
        public MetaController(IConfiguration config)
        {
            _config = config;
        }

        // TODO: Add cache - [ResponseCache(Duration = 3600)]

        #region GET

        #region BASICS
        [HttpGet("instagram/id")]
        public async Task<IActionResult> GetInstagramId()
        {
            var token = _config["Instagram:Token"];
            var url = $"https://graph.instagram.com/me?access_token={token}";

            var client = new HttpClient();
            var response = await client.GetStringAsync(url);

            return Content(response, "application/json");
        }

        [HttpGet("instagram")]
        public async Task<IActionResult> GetInstagram()
        {
            var token = _config["Instagram:Token"];
            var url = $"https://graph.instagram.com/me/media?fields=id,caption,media_url,permalink&access_token={token}";

            var client = new HttpClient();
            var response = await client.GetStringAsync(url);

            return Content(response, "application/json");
        }
        #endregion

        #region PROFILE
        [HttpGet("instagram/profile-info/basic/{id}")]
        public async Task<IActionResult> GetInstagramProfileBasicInfo(string id)
        {
            var token = _config["Instagram:Token"];
            var url = $"https://graph.instagram.com/{id}?fields=id,username,name,biography,profile_picture_url,website&access_token={token}";

            var client = new HttpClient();
            var response = await client.GetStringAsync(url);

            return Content(response, "application/json");
        }

        [HttpGet("instagram/profile-info/stats/{id}")]
        public async Task<IActionResult> GetInstagramProfileStatsInfo(string id)
        {
            var token = _config["Instagram:Token"];
            var url = $"https://graph.instagram.com/{id}?fields=followers_count,follows_count,media_count&access_token={token}";

            var client = new HttpClient();
            var response = await client.GetStringAsync(url);

            return Content(response, "application/json");
        }

        [HttpGet("instagram/profile-info/business/{id}")]
        public async Task<IActionResult> GetInstagramProfileBusinessInfo(string id)
        {
            var token = _config["Instagram:Token"];
            var url = $"https://graph.instagram.com/{id}?fields=account_type,business_category_name,category,contact_phone_number,email,address_street,city_name,zip,contact_phone_number&access_token={token}";

            var client = new HttpClient();

            try
            {
                var response = await client.GetStringAsync(url);

                return Content(response, "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        #endregion

        #region POSTS
        [HttpGet("instagram/posts")]
        public async Task<IActionResult> GetInstagramPosts()
        {
            var token = _config["Instagram:Token"];
            var accountId = "25878322951867942";
            var url = $"https://graph.instagram.com/{accountId}/media?fields=id,caption,media_url,media_type,permalink&access_token={token}";

            var client = new HttpClient();
            var response = await client.GetStringAsync(url);

            return Content(response, "application/json");
        }

        [HttpGet("instagram/posts/{id}")]
        public async Task<IActionResult> GetInstagramPostsById(string id)
        {
            var token = _config["Instagram:Token"];
            var url = $"https://graph.instagram.com/{id}?fields=id,caption,media_url,like_count,comments_count&access_token={token}";

            var client = new HttpClient();
            var response = await client.GetStringAsync(url);

            return Content(response, "application/json");
        }
        #endregion

        #region DATA & ANALYTICS
        [HttpGet("instagram/insights/{id}")]
        public async Task<IActionResult> GetInstagramInsights(string id)
        {
            var token = _config["Instagram:Token"];
            var url = $"https://graph.instagram.com/{id}/insights&access_token={token}";

            var client = new HttpClient();
            var response = await client.GetStringAsync(url);

            return Content(response, "application/json");
        }
        #endregion

        #endregion

        #region POST

        #region INSTAGRAM POSTS
        [HttpPost("instagram/post/media-upload")]
        public async Task<IActionResult> PostMediaUpload([FromBody] PostStaticMedia media)
        {
            var token = _config["Instagram:Token"];
            media.InstagramUserToken = token;
            var url = $"https://graph.instagram.com/{media.InstagramUserId}/media";

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("access_token", media.InstagramUserToken!),
                new KeyValuePair<string, string>("image_url", media.URL),
                new KeyValuePair<string, string>("is_carousel_item", "false"),
                new KeyValuePair<string, string>("alt_text", "🤖 Testing Meta'\''s IG Graph API...bagulho doido"),
                new KeyValuePair<string, string>("caption", "🤖 Testing Meta'\''s IG Graph API...bagulho doido")
            });

            var client = new HttpClient();
            try
            {
                var response = await client.PostAsync(url, content);

                return Content(await response.Content.ReadAsStringAsync(), "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("instagram/post/media-publish")]
        public async Task<IActionResult> PostMediaPublish([FromBody] PostStaticMedia media)
        {
            var token = _config["Instagram:Token"];
            media.InstagramUserToken = token;
            var url = "https://graph.instagram.com/me/media_publish";

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("access_token", media.InstagramUserToken),
                new KeyValuePair<string, string>("media_url", media.URL),
                new KeyValuePair<string, string>("creation_id", "18422459899140904"),
                new KeyValuePair<string, string>("caption", media.Caption)
            });

            var client = new HttpClient();
            try
            {
                var response = await client.PostAsync(url, content);

                return Content(await response.Content.ReadAsStringAsync(), "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("instagram/post/media-comment")]
        public async Task<IActionResult> PostMediaComment([FromBody] MediaComment comment)
        {
            var token = _config["Instagram:Token"];
            var url = $"https://graph.instagram.com/{comment.PostId}/comments";

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("access_token", token!),
                new KeyValuePair<string, string>("message", comment.CommentMessage),

            });

            var client = new HttpClient();
            try
            {
                var response = await client.PostAsync(url, content);

                return Content(await response.Content.ReadAsStringAsync(), "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #endregion
    }
}
