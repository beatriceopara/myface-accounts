using System;
using System.Data.Entity;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFace.Models.Database;
using MyFace.Models.Request;
using MyFace.Models.Response;
using MyFace.Repositories;
using MyFace.Services;

namespace MyFace.Controllers
{
    [ApiController]
    [Route("/interactions")]
    public class InteractionsController : Controller
    {
        private readonly IInteractionsRepo _interactions;
        private readonly IUsersRepo _users;
        private readonly IHashService _hashService;

        public InteractionsController(IInteractionsRepo interactions, IUsersRepo users, IHashService hashService)
        {
            _interactions = interactions;
            _users = users;
            _hashService = hashService;
        }
    
        [HttpGet("")]
        public ActionResult<ListResponse<InteractionResponse>> Search([FromQuery] SearchRequest search)
        {
            var interactions = _interactions.Search(search);
            var interactionCount = _interactions.Count(search);
            return InteractionListResponse.Create(search, interactions, interactionCount);
        }

        [HttpGet("{id}")]
        public ActionResult<InteractionResponse> GetById([FromRoute] int id)
        {
            var interaction = _interactions.GetById(id);
            return new InteractionResponse(interaction);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateInteractionRequest newUser)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            var interaction = _interactions.Create(newUser);

            var url = Url.Action("GetById", new { id = interaction.Id });
            var responseViewModel = new InteractionResponse(interaction);
            
            CheckAuthHeader();
            return Created(url, responseViewModel);
        }
        //create checkAuthHeader() -- can move into new class if want to reuse 

        public string CheckAuthHeader()
        {
            // Base64Encode("mikewalker:secret-password")
            // Content-Type => "application/json"
            // Authorization => "Basic aolhtowyosihfsoi=="
            string authHeader = Request.Headers["Authorization"];

            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                string encodedUsernamePassword = authHeader.Substring("Basic".Length).Trim();
                
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

                int separatorIndex = usernamePassword.IndexOf(':');

                var username = usernamePassword.Substring(0, separatorIndex);
                var password = usernamePassword.Substring(separatorIndex + 1);

                User user = _users.GetByUsername(username);

                var originalPassword = user.HashedPassword;

                var salt = _hashService.GetSalt();
                
                var attemptedPassword = _hashService.HashPassword(salt, password);
                
                //password = salt, hashpassword (not same)
                //hash password and compare what is in the Database
                if (attemptedPassword != originalPassword)
                {
                    // return Unauthorized().ToString();
                    return user.Username;
                    // return Ok();
                    //continue request
                }
             
            }
            else
            {
                throw new Exception(Unauthorized(401).ToString());
                // throw new Exception("The authorization header is either empty or isn't Basic.");
                // return Unauthorized().ToString();
            }

            return authHeader;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _interactions.Delete(id);
            return Ok();
        }
    }
}