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
        private readonly IAuthService _authService;

        public FeedController(IPostsRepo posts, IAuthService authService)
        {
            _posts = posts;
            _authService = authService;
        }
        
        [HttpGet("")]
        public ActionResult<FeedModel> GetFeed([FromQuery] SearchRequest searchRequest)
        {
            // if (!_authService.HasValidAuthorization(Request))
            // {
            //     return UnauthorizedResult();
            // }
            var posts = _posts.SearchFeed(searchRequest);
            var postCount = _posts.Count(searchRequest);
            return FeedModel.Create(searchRequest, posts, postCount);
        }
    }
}