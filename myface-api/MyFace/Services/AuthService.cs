using Microsoft.AspNetCore.Http;
using MyFace.Repositories;

namespace MyFace.Services
{
    public interface IAuthService
    {
        bool HasValidAuthorization(HttpRequest request);
    }
    
    public class AuthService: IAuthService
    {
        private IUsersRepo _users;
        private const string AUTH_HEADER_NAME = "Authorization";
        private const string BASIC_HEADER_NAME = "Basic";

        public AuthService(IUsersRepo users)
        {
            _users = users;
        }

        public bool HasValidAuthorization(HttpRequest request)
        {
            if (!request.Headers.ContainsKey(AUTH_HEADER_NAME))
            {
                return false;
            }

            var authHeader = request.Headers[AUTH_HEADER_NAME];
            var authHeaderValue = authHeader[0];

            if (!authHeaderValue.StartsWith(BASIC_HEADER_NAME))
            {
                return false;
            }

            var encodedString = authHeaderValue.Substring(BASIC_HEADER_NAME.Length);
            return true;
        }
    }
}