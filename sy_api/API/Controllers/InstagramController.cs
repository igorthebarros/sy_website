using InstagramAPI.DTOs;
using MetaService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MetaAPI.Controllers
{
    public class InstagramController : ControllerBase
    {
        private readonly IInstagramService _service;
        public InstagramController(IInstagramService service)
        {
            _service = service;
        }

        // TODO: Add cache - [ResponseCache(Duration = 3600)]
        // TODO: Use InstagramPathConstant for endpoints

        #region BASICS
        [HttpGet("instagram/id")]
        public async Task<IActionResult> GetInstagramAccountId()
        {
            try
            {
                var response = await _service.GetAccountIdAsync();

                return Content(response, "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("instagram")]
        public async Task<IActionResult> GetInstagramAccount()
        {
            try
            {
                var response = await _service.GetAccountAsync();

                return Content(response, "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("instagram/profile-info/basic/{id}")]
        public async Task<IActionResult> GetInstagramProfileBasicInfo(string id)
        {
            try
            {
                var response = await _service.GetProfileBasicAsync(id);

                return Content(response, "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("instagram/profile-info/stats")]
        public async Task<IActionResult> GetInstagramProfileStatsInfo()
        {
            try
            {
                var response = await _service.GetProfileStatsInfoAsync();

                return Content(response, "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("instagram/profile-info/business/{id}")]
        public async Task<IActionResult> GetInstagramProfileBusinessInfo(string id)
        {
            try
            {
                var response = await _service.GetProfileBusinessInfoAsync();

                return Content(response, "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region INSTAGRAM POSTS
        [HttpPost("instagram/post/media-upload")]
        public async Task<IActionResult> PostInstagramMediaUpload([FromBody] PostMediaDTO media)
        {
            try
            {
                var response = await _service.GetPostsAsync();

                return Content(response, "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            //var token = _config["Instagram:Token"];
            //media.InstagramUserToken = token;
            //var url = $"https://graph.instagram.com/{media.InstagramUserId}/media";

            //var content = new FormUrlEncodedContent(new[]
            //{
            //    new KeyValuePair<string, string>("access_token", media.InstagramUserToken!),
            //    new KeyValuePair<string, string>("image_url", media.URL),
            //    new KeyValuePair<string, string>("is_carousel_item", "false"),
            //    new KeyValuePair<string, string>("alt_text", "🤖 Testing Meta'\''s IG Graph API...bagulho doido"),
            //    new KeyValuePair<string, string>("caption", "🤖 Testing Meta'\''s IG Graph API...bagulho doido")
            //});

            //var client = new HttpClient();
            //try
            //{
            //    var response = await client.PostAsync(url, content);

            //    return Content(await response.Content.ReadAsStringAsync(), "application/json");
            //}
            //catch (Exception e)
            //{
            //    return BadRequest(e.Message);
            //}
        }

        [HttpPost("instagram/post/media-publish")]
        public async Task<IActionResult> PostInstagramMediaPublish([FromBody] PostMediaDTO media)
        {
            try
            {
                var response = await _service.GetPostsAsync();

                return Content(response, "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            //var token = _config["Instagram:Token"];
            //media.InstagramUserToken = token;
            //var url = "https://graph.instagram.com/me/media_publish";

            //var content = new FormUrlEncodedContent(new[]
            //{
            //    new KeyValuePair<string, string>("access_token", media.InstagramUserToken),
            //    new KeyValuePair<string, string>("media_url", media.URL),
            //    new KeyValuePair<string, string>("creation_id", "18422459899140904"),
            //    new KeyValuePair<string, string>("caption", media.Caption)
            //});

            //var client = new HttpClient();
            //try
            //{
            //    var response = await client.PostAsync(url, content);

            //    return Content(await response.Content.ReadAsStringAsync(), "application/json");
            //}
            //catch (Exception e)
            //{
            //    return BadRequest(e.Message);
            //}
        }

        [HttpPost("instagram/post/media-comment")]
        public async Task<IActionResult> PostInstagramMediaComment([FromBody] InstagramDTO comment)
        {
            try
            {
                var response = await _service.GetPostsAsync();

                return Content(response, "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            //var token = _config["Instagram:Token"];
            //var url = $"https://graph.instagram.com/{comment.PostId}/comments";

            //var content = new FormUrlEncodedContent(new[]
            //{
            //    new KeyValuePair<string, string>("access_token", token!),
            //    new KeyValuePair<string, string>("message", comment.CommentMessage),

            //});

            //var client = new HttpClient();
            //try
            //{
            //    var response = await client.PostAsync(url, content);

            //    return Content(await response.Content.ReadAsStringAsync(), "application/json");
            //}
            //catch (Exception e)
            //{
            //    return BadRequest(e.Message);
            //}
        }

        [HttpGet("instagram/posts")]
        public async Task<IActionResult> GetInstagramPosts()
        {
            try
            {
                var response = await _service.GetPostsAsync();

                return Content(response, "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("instagram/posts/{id}")]
        public async Task<IActionResult> GetInstagramPostsById(string id)
        {
            try
            {
                var response = await _service.GetPostsAsync();

                return Content(response, "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region DATA & ANALYTICS
        [HttpGet("instagram/insights/{id}")]
        public async Task<IActionResult> GetInstagramInsights(string id)
        {
            try
            {
                var response = await _service.GetPostsAsync();

                return Content(response, "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

    }
}
