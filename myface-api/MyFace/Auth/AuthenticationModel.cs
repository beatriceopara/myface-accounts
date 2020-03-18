using System.ComponentModel.DataAnnotations;

namespace MyFace.Auth
{
    public class AuthenticationModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}


// check that the username and password given are correct.
// If username and password correct return token
// else return BadRequest();
//decode the username and password
                
/*grab auth header from request Request.Headers.get
parse / decode header to get username and password
check username and password valid
if not then return Forbidden();
Request.Headers["Authorization"]*/