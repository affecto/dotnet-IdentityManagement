using Affecto.Authentication.Claims;
using NSubstitute;

namespace Affecto.IdentityManagement.AcceptanceTests.Infrastructure
{
    internal class MockAuthenticatedUserContext
    {
        private readonly IAuthenticatedUserContext mockUserContext;

        public MockAuthenticatedUserContext(IAuthenticatedUserContext mockUserContext)
        {
            this.mockUserContext = mockUserContext;
        }

        public void FailPermissionCheck(string permission)
        {
            mockUserContext.When(x => x.CheckPermission(permission)).Do(x => { throw new InsufficientPermissionsException(permission); });
        }
    }
}