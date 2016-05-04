using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.Authentication.Claims;
using Affecto.IdentityManagement.AcceptanceTests.Infrastructure;
using Affecto.IdentityManagement.Interfaces.Exceptions;
using Affecto.IdentityManagement.Interfaces.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Affecto.IdentityManagement.AcceptanceTests.Features
{
    [Binding]
    [Scope(Feature = "Users")]
    internal class UsersSteps : StepDefinition
    {
        private List<IUser> returnedUsers;
            
        [Given(@"a user '(.+)' is added")]
        [When(@"a user '(.+)' is added")]
        public void WhenAUserIsAdded(string userName)
        {
            Try(() =>
            {
                IUserListItem userListItem = IdentityManagementService.CreateUser(userName);
                nameIdentifierPairs.Add(userName, userListItem.Id);
            });
        }

        [When(@"a user '(.+)' is added with the following custom properties:")]
        public void WhenAUserIsAdded(string userName, Table customPropertiesTable)
        {
            Try(() =>
            {
                CreateUser(userName, customPropertiesTable);
            });
        }

        [Given(@"a user '(.+)' is added with the following custom properties:")]
        public void GivenAUserIsAdded(string userName, Table customPropertiesTable)
        {
            CreateUser(userName, customPropertiesTable);
        }

        [When(@"users are searched by custom property name '(.+)' and value '(.+)'")]
        public void WhenUsersAreSearchedByCustomPropertyNameAndValue(string customPropertyName, string customPropertyValue)
        {
            Try(() =>
            {
                returnedUsers = IdentityManagementService.GetUsers(customPropertyName, customPropertyValue).ToList();
            });
        }

        [When(@"active directory users are searched by custom property name '(.+)' and value '(.+)'")]
        public void WhenExternalActiveDirectoryUsersAreSearchedByCustomPropertyNameAndValue(string customPropertyName, string customPropertyValue)
        {
            Try(() =>
            {
                returnedUsers = IdentityManagementService.GetUsers(customPropertyName, customPropertyValue, AccountType.ActiveDirectory).ToList();
            });
        }

        [Then(@"the following users having custom properties are returned:")]
        public void ThenTheUserIsReturnedHavingTheFollowingCustomProperties(Table users)
        {
            Assert.IsNotNull(returnedUsers);
            Assert.AreEqual(users.RowCount, returnedUsers.Count);

            foreach (IUser user in returnedUsers)
            {
                Assert.IsNotNull(user.CustomProperties);
                Assert.AreEqual(3, user.CustomProperties.Count);

                string emailAddress = user.CustomProperties.Where(c => c.Name == "EmailAddress").Select(c => c.Value).Single();
                string organizationId = user.CustomProperties.Where(c => c.Name == "OrganizationId").Select(c => c.Value).Single();
                string streetAddress = user.CustomProperties.Where(c => c.Name == "StreetAddress").Select(c => c.Value).Single();

                Assert.IsTrue(users.Rows.Any(u =>
                    u["Name"] == user.Name && u["EmailAddress"] == emailAddress && u["OrganizationId"] == organizationId
                    && u["StreetAddress"] == streetAddress));
            }
        }

        [Then(@"all users are enabled")]
        public void ThenAllUsersAreEnabled()
        {
            IEnumerable<IUserListItem> users = IdentityManagementService.GetUsers();
            foreach (IUserListItem user in users)
            {
                Assert.IsFalse(user.IsDisabled, user.Name);
            }
        }

        [When(@"the user '(.+)' is disabled")]
        public void WhenTheUserIsDisabled(string name)
        {
            Try(() => IdentityManagementService.UpdateUser(nameIdentifierPairs[name], name, true));
        }

        [Then(@"the user '(.+)' is disabled")]
        public void ThenTheUserIsDisabled(string name)
        {
            IUser user = IdentityManagementService.GetUser(nameIdentifierPairs[name]);
            Assert.IsTrue(user.IsDisabled);
        }

        [When(@"the name of the user '(.+)' is changed to '(.+)'")]
        public void WhenTheNameOfTheUserIsChangedTo(string previousName, string currentName)
        {
            Try(() => IdentityManagementService.UpdateUser(nameIdentifierPairs[previousName], currentName, false));
        }

        [Given(@"the user '(.+)' is given the role '(.+)'")]
        [When(@"the user '(.+)' is given the role '(.+)'")]
        public void WhenTheUserIsGivenTheRole(string name, string role)
        {
            Guid roleId = MockDatabase.GetRole(role).Id;
            Try(() => IdentityManagementService.AddUserRole(nameIdentifierPairs[name], roleId));
        }

        [When(@"the user '(.+)' is removed from the role '(.+)'")]
        public void WhenTheUserIsRemovedFromTheRole(string user, string role)
        {
            Guid roleId = MockDatabase.GetRole(role).Id;
            Try(() => IdentityManagementService.RemoveUserRole(nameIdentifierPairs[user], roleId));
        }

        [Then(@"the user '(.+)' has no roles")]
        public void ThenTheUserHasNoRoles(string user)
        {
            IEnumerable<IRole> roles = IdentityManagementService.GetUser(nameIdentifierPairs[user]).Roles;
            Assert.IsFalse(roles.Any());
        }

        [When(@"a user with no name is added")]
        public void WhenAUserWithNoNameIsAdded()
        {
            Try(() => IdentityManagementService.CreateUser(string.Empty));
        }

        [Then(@"adding the user fails")]
        [Then(@"updating the user fails")]
        public void ThenAddingTheUserFails()
        {
            AssertCaughtException<ArgumentException>();
        }

        [When(@"the name of the user '(.+)' is cleared")]
        public void WhenTheNameOfTheUserIsCleared(string user)
        {
            Guid userId = nameIdentifierPairs[user];
            Try(() => IdentityManagementService.UpdateUser(userId, string.Empty, false));
        }

        [Given(@"an organization '(.+)' exists")]
        public void GivenAnOrganizationExists(string organization)
        {
            MockDatabase.AddOrganization(organization);
        }

        [Given(@"an disabled organization '(.+)' exists")]
        public void GivenAnDisabledOrganizationExists(string organization)
        {
            MockDatabase.AddOrganization(organization, true);
        }

        [Given(@"the user '(.+)' is added to the organization '(.+)'")]
        [When(@"the user '(.+)' is added to the organization '(.+)'")]
        public void WhenTheUserIsAddedToTheOrganization(string user, string organization)
        {
            Guid organizationId = MockDatabase.GetOrganization(organization).Id;
            Guid userId = nameIdentifierPairs[user];
            Try(() => IdentityManagementService.AddUserOrganization(userId, organizationId));
        }

        [Given(@"an active directory account with name '(.+)' is added for user '(.+)'")]
        public void GivenAnActiveDirectoryAccountWithNameIsAddedForUser(string name, string user)
        {
            AddUserAccount(AccountType.ActiveDirectory, name, user);
        }

        [When(@"an active directory account with name '(.+)' is added for user '(.+)'")]
        public void WhenAnActiveDirectoryAccountWithNameIsAddedForUser(string name, string user)
        {
            Try(() => AddUserAccount(AccountType.ActiveDirectory, name, user));
        }

        [Given(@"an account with name '(.+)' and password '(.+)' is added for user '(.+)'")]
        public void GivenAnAccountWithNameAndPasswordIsAddedForUser(string accountName, string password, string userName)
        {
            AddUserAccount(accountName, password, userName);
        }

        [When(@"an account with name '(.+)' and password '(.+)' is added for user '(.+)'")]
        public void WhenAnAccountWithNameAndPasswordIsAddedForUser(string accountName, string password, string userName)
        {
            Try(() => AddUserAccount(accountName, password, userName));
        }

        [When(@"a password account with name '(.+)' without password is added for user '(.+)'")]
        public void WhenAPasswordAccountWithNameWithoutPasswordIsAddedForUser(string name, string user)
        {
            Try(() => AddUserAccount(AccountType.Password, name, user));
        }

        [Then(@"password '(.+)' matches to the password of user account '(.+)'")]
        public void ThenPasswordMatchesToThePasswordOfUserAccount(string password, string accountName)
        {
            Assert.IsTrue(UserService.IsMatchingPassword(accountName, password));
        }

        [Then(@"password '(.+)' does not match to the password of user account '(.+)'")]
        public void ThenPasswordDoesNotMatchToThePasswordOfUserAccount(string password, string accountName)
        {
            Assert.IsFalse(UserService.IsMatchingPassword(accountName, password));
        }

        [When(@"a federated account with name '(.+)' is added for user '(.+)'")]
        public void WhenAFederatedAccountWithNameIsAddedForUser(string name, string user)
        {
            Try(() => AddUserAccount(AccountType.Federated, name, user));
        }

        [When(@"an active directory account without name is added for user '(.+)'")]
        public void WhenAnActiveDirectoryAccountWithoutNameIsAddedForUser(string user)
        {
            Try(() => GivenAnActiveDirectoryAccountWithNameIsAddedForUser(string.Empty, user));
        }

        [When(@"removing active directory account from user '(.+)'")]
        public void WhenRemovingActiveDirectoryAccountFromUser(string user)
        {
            Try(() =>
            {
                Guid userId = nameIdentifierPairs[user];
                var name = MockDatabase.GetUser(user).Accounts.Single(o => o.Type == Store.Model.AccountType.ActiveDirectory).Name;
                IdentityManagementService.RemoveUserAccount(userId, AccountType.ActiveDirectory, name);
            });
        }

        [Then(@"the user '(.+)' is in the following organizations:")]
        public void ThenTheUserIsInTheFollowingOrganizations(string userName, Table expectedOrganizations)
        {
            IUser user = IdentityManagementService.GetUser(nameIdentifierPairs[userName]);
            Assert.AreEqual(expectedOrganizations.RowCount, user.Organizations.Count);
            foreach (TableRow row in expectedOrganizations.Rows)
            {
                Assert.IsNotNull(user.Organizations.SingleOrDefault(o => o.Name == row["Organization"]));
            }
        }

        [When(@"the user '(.+)' is removed from the organization '(.+)'")]
        public void WhenTheUserIsRemovedFromTheOrganization(string user, string organization)
        {
            Guid organizationId = MockDatabase.GetOrganization(organization).Id;
            Guid userId = nameIdentifierPairs[user];
            Try(() => IdentityManagementService.RemoveUserOrganization(userId, organizationId));
        }

        [When(@"active directory account name is changed to '(.+)' for user '(.+)'")]
        public void WhenActiveDirectoryAccountNameIsChangedToForUser(string accountName, string userName)
        {
            Guid userId = nameIdentifierPairs[userName];
            IUser user = IdentityManagementService.GetUser(userId);

            Try(() => IdentityManagementService.UpdateExternalUserAccount(user.Id, AccountType.ActiveDirectory, accountName));
        }

        [When(@"active directory account name is cleared for user '(.+)'")]
        public void WhenActiveDirectoryAccountNameIsClearedForUser(string userName)
        {
            WhenActiveDirectoryAccountNameIsChangedToForUser(string.Empty, userName);
        }

        [Then(@"the user '(.+)' is in no organization")]
        public void ThenTheUserIsInNoOrganization(string userName)
        {
            IUser user = IdentityManagementService.GetUser(nameIdentifierPairs[userName]);
            Assert.AreEqual(0, user.Organizations.Count);
        }

        [Then(@"removing the organization fails because of invalid permissions")]
        [Then(@"adding the organization fails because of invalid permissions")]
        [Then(@"removing the role fails because of invalid permissions")]
        [Then(@"adding the role fails because of invalid permissions")]
        [Then(@"updating the user fails because of invalid permissions")]
        [Then(@"adding the user fails because of invalid permissions")]
        public void ThenRemovingTheOrganizationFailsBecauseOfInvalidPermissions()
        {
            AssertCaughtException<InsufficientPermissionsException>();
        }

        [Then(@"adding new account fails because of account's name is not specified")]
        [Then(@"updating account fails because of account's name is not specified")]
        [Then(@"adding new account fails because wrong account type is specified")]
        public void ThenAddingNewAccountFailsBecauseOfAccountWithSameTypeAlreadyExists()
        {
            AssertCaughtException<ArgumentException>();
        }

        [Then(@"adding the organization fails")]
        public void ThenAddingTheOrganizationFails()
        {
            AssertCaughtException<ArgumentException>();
        }

        [Then(@"adding new account fails because account is already assigned")]
        public void ThenAddingNewAccountFailsBecauseAccountIsAlreadyAssigned()
        {
            AssertCaughtException<UserAccountAlreadyAssignedException>();
        }

        private void AddUserAccount(AccountType type, string accountName, string userName)
        {
            Guid userId = nameIdentifierPairs[userName];
            IdentityManagementService.AddExternalUserAccount(userId, type, accountName);
        }

        private void AddUserAccount(string accountName, string password, string userName)
        {
            Guid userId = nameIdentifierPairs[userName];
            IdentityManagementService.AddUserAccount(userId, accountName, password);
        }

        private void CreateUser(string userName, Table customPropertiesTable)
        {
            List<KeyValuePair<string, string>> customProperties = customPropertiesTable.Rows
                .Select(row => new KeyValuePair<string, string>(row["Name"], row["Value"]))
                .ToList();

            IUserListItem userListItem = IdentityManagementService.CreateUser(userName, customProperties);
            nameIdentifierPairs.Add(userName, userListItem.Id);
        }
    }
}