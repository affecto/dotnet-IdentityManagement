using Affecto.IdentityManagement.AcceptanceTests.Infrastructure;
using Affecto.IdentityManagement.ApplicationServices;
using TechTalk.SpecFlow;

namespace Affecto.IdentityManagement.AcceptanceTests.Steps
{
    [Binding]
    internal class AuthenticatedUserContextSteps : StepDefinition
    {
        [Given(@"the user has no permission to maintain user data")]
        [When(@"the user has no permission to maintain user data")]
        public void GivenTheUserHasNoPermissionToMaintainUserData()
        {
            MockAuthenticatedUserContext.FailPermissionCheck(Permissions.UserMaintenance);
        }

        [Given(@"the user has no permission to maintain role permissions")]
        [When(@"the user has no permission to maintain role permissions")]
        public void GivenTheUserHasNoPermissionToMaintainRolePermissions()
        {
            MockAuthenticatedUserContext.FailPermissionCheck(Permissions.RoleMaintenance);
        }
    }
}