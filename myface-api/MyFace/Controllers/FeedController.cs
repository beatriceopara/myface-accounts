using Microsoft.AspNetCore.Mvc;
using MyFace.Services;
using MyFace.Models.Request;
using MyFace.Models.Response;
using MyFace.Repositories;

namespace MyFace.Controllers
{
    [Route("feed")]
    public class FeedController
    {
        private readonly IPostsRepo _posts;
       

        public FeedController(IPostsRepo posts)
        {
            _posts = posts;
        }
        
        [HttpGet("")]
        public ActionResult<FeedModel> GetFeed([FromQuery] SearchRequest searchRequest)
        {
            var posts = _posts.SearchFeed(searchRequest);
            var postCount = _posts.Count(searchRequest);
            return FeedModel.Create(searchRequest, posts, postCount);
        }
    }
}