using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace veebipood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public PhotoController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public async Task<ActionResult<List<Photo>>> GetPhotos()
        {
            var response = await _httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/photos");
            var photos = JsonConvert.DeserializeObject<List<Photo>>(response);

            return Ok(photos);
        }
    }

    public class Photo
    {
        public int AlbumId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}

