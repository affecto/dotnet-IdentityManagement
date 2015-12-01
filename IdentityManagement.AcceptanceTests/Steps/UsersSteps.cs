using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.IdentityManagement.AcceptanceTests.Infrastructure;
using Affecto.IdentityManagement.Interfaces.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Affecto.IdentityManagement.AcceptanceTests.Steps
{
    [Binding]
    internal class UsersSteps : StepDefinition
    {
        [Then(@"the following users exist:")]
        public void ThenTheFollowingUsersExist(Table expectedUsers)
        {
            List<IUserListItem> users = IdentityManagementService.GetUsers().ToList();

            Assert.AreEqual(expectedUsers.RowCount, users.Count);

            foreach (TableRow expectedUser in expectedUsers.Rows)
            {
                Assert.IsTrue(users.Any(u => u.Name.Equals(expectedUser["Name"])), expectedUser.ToTableString());
            }
        }

        [Then(@"user '(.+)' has an active directory account with name '(.+)'")]
        public void ThenUserHasAnActiveDirectoryAccountWithName(string userName, string accountName)
        {
            Guid userId = MockDatabase.GetUser(userName).Id;
            IEnumerable<IAccount> accounts = IdentityManagementService.GetUserAccounts(userId);

            Assert.AreEqual(1, accounts.Count(a => a.Type == AccountType.ActiveDirectory && a.Name == accountName));
        }

        [Then(@"user '(.+)' has an account with name '(.+)' and password '(.+)'")]
        public void ThenUserHasAnAccountWithNameAndPassword(string userName, string accountName, string password)
        {
            Guid userId = MockDatabase.GetUser(userName).Id;
            IEnumerable<IAccount> accounts = IdentityManagementService.GetUserAccounts(userId);

            IAccount account = accounts.SingleOrDefault(a => a.Type == AccountType.Password && a.Name == accountName);

            Assert.IsNotNull(account);
            Assert.IsTrue(UserService.IsMatchingPassword(accountName, password));
        }

        [Then(@"user '(.+)' has a federated account with name '(.+)'")]
        public void ThenUserHasAFederatedAccountWithName(string userName, string accountName)
        {
            Guid userId = MockDatabase.GetUser(userName).Id;
            IEnumerable<IAccount> accounts = IdentityManagementService.GetUserAccounts(userId);

            Assert.AreEqual(1, accounts.Count(a => a.Type == AccountType.Federated && a.Name == accountName));
        }

        [Then(@"user '(.+)' has no assigned active directory account")]
        public void ThenUserHasNoAssignedActiveDirectoryAccount(string userName)
        {
            Guid userId = MockDatabase.GetUser(userName).Id;
            IEnumerable<IAccount> accounts = IdentityManagementService.GetUserAccounts(userId);
            Assert.IsFalse(accounts.Any(o => o.Type == AccountType.Federated));
        }

        [Then(@"the user '(.+)' has the following roles:")]
        public void ThenTheUserHasTheFollowingRoles(string userName, Table expectedRoles)
        {
            Guid userId = MockDatabase.GetUser(userName).Id;
            IReadOnlyCollection<IRole> roles = IdentityManagementService.GetUser(userId).Roles;

            Assert.AreEqual(expectedRoles.RowCount, roles.Count);

            foreach (TableRow expectedRole in expectedRoles.Rows)
            {
                Assert.IsTrue(roles.Any(r => r.Name.Equals(expectedRole["Role"])));
            }
        }

        [Then(@"the user '(.+)' has the following custom properties:")]
        public void ThenTheUserHasTheFollowingCustomProperties(string userName, Table expectedCustomProperties)
        {
            Guid userId = MockDatabase.GetUser(userName).Id;
            IReadOnlyCollection<ICustomProperty> customProperties = IdentityManagementService.GetUser(userId).CustomProperties;

            Assert.AreEqual(expectedCustomProperties.RowCount, customProperties.Count);

            foreach (TableRow expectedCustomProperty in expectedCustomProperties.Rows)
            {
                Assert.IsTrue(customProperties.Any(c => c.Name == expectedCustomProperty["Name"] && c.Value == expectedCustomProperty["Value"]));
            }
        }
    }
}