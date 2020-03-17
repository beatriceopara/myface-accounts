using System.ComponentModel.DataAnnotations;

namespace MyFace.Auth
{
    public class AuthenticationModel
    {
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}