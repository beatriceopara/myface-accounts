using System;
using System.Data.Entity;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFace.Models.Request;
using MyFace.Models.Response;
using MyFace.Repositories;

namespace MyFace.Controllers
{
    public class InteractionsController
    {
        [ApiController]
        [Route("/interactions")]
        public class UsersController : ControllerBase
        {
            private readonly IInteractionsRepo _interactions;

            public UsersController(IInteractionsRepo interactions)
            {
                _interactions = interactions;
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
                string authHeader = Request.Headers["Authorization"];

                if (authHeader != null && authHeader.StartsWith("Basic"))
                {
                    string encodedUsernamePassword = authHeader.Substring("Basic".Length).Trim();
                    Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                    string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

                    int separatorIndex = usernamePassword.IndexOf(':');

                    var username = usernamePassword.Substring(0, separatorIndex);
                    var password = usernamePassword.Substring(separatorIndex + 1);

                    // UsersRepo.Count(username);
                    //
                    // //if username found in database
                    // //hash password and compare to what is in DB
                    // //if same
                    // if ()
                    // {
                    //     
                    // }

                }
                else
                {
                    throw new Exception("The authorization header is either empty or isn't Basic.");
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
}