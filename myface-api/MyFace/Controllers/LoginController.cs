using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyFace.Auth;
using MyFace.Data;
using MyFace.Repositories;

namespace MyFace.Controllers
{    
    [ApiController]
    // [Authorize]
    [Route("/login")]
    public class LoginController:ControllerBase
    {
        private readonly IUsersRepo _users;
        
        public LoginController(IUsersRepo users)
        {
            _users = users;
        }
        
        //pick an endpoint to add Basic Auth - choosing endpoint and adding a restriction - only used by the person, user/pwd valid
        [HttpGet("authenticate")]
        public IActionResult Authenticate(string username, string password)
        {
            
            //https://jasonwatmore.com/post/2019/10/11/aspnet-core-3-jwt-authentication-tutorial-with-example-api#authenticate-model-cs

            //https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-configuration?view=aspnetcore-3.1
           
            // check that the username and password given are correct.
            // If username and password correct return token
            // else return BadRequest();
        }
        
        //decode the username and password
      
        
    }
}