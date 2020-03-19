using FakeItEasy;
using MyFace.Repositories;
using MyFace.Services;
using NUnit.Framework;

namespace MyFace.Test
{
    public class AuthServiceTests
    {
        private AuthService _authService;
        private IUsersRepo _fakeUsersRepo;

        [SetUp]
        public void SetUp()
        {
            _fakeUsersRepo = A.Fake<IUsersRepo>();
        }
    }
}