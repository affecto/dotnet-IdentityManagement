using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.IdentityManagement.AcceptanceTests.Infrastructure;
using Affecto.IdentityManagement.Interfaces.Model;
using Affecto.IdentityManagement.Querying.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Affecto.IdentityManagement.AcceptanceTests.Features
{
    [Binding]
    [Scope(Feature = "ExternalUsers")]
    internal class ExternalUsersSteps : StepDefinition
    {
        private readonly List<ExternalUser> externalUsers = new List<ExternalUser>();

        class ExternalUser
        {
            public string userName;
            public string displayName;
            public string accountName;
            public AccountType accountType;
            public readonly List<string> memberOfGroups = new List<string>();
        }

        [Given(@"there are following external users:")]
        public void GivenThereAreFollowingExternalUsers(Table table)
        {
            foreach (var row in table.Rows)
            {
                var externalUser = new ExternalUser
                {
                    userName = row["User name"],
                    displayName = row["Display name"],
                    accountName = row["Account name"],
                    accountType = ResolveAccountType(row["Account type"])
                };
                externalUsers.Add(externalUser);
            }
        }

        [Given(@"external user '(.+)' is member of following groups:")]
        public void GivenExternalUserIsMemberOfFollowingGroups(string userName, Table table)
        {
            ExternalUser user = GetExternalUser(userName);
            foreach (var row in table.Rows)
            {
                user.memberOfGroups.Add(row["Group"]);
            }
        }

        [Given(@"there are following groups in the identity management service:")]
        public void GivenThereAreFollowingGroupsInTheIdentityManagementService(Table table)
        {
            foreach (var row in table.Rows)
            {
                MockDatabase.AddGroup(row["Name"], null, row["External group"]);
            }
        }

        [Given(@"there are following roles in the identity management service:")]
        public void GivenThereAreFollowingRolesInTheIdentityManagementService(Table table)
        {
            foreach (var row in table.Rows)
            {
                MockDatabase.AddRole(row["Name"], null, row["External group"]);
            }
        }

        [Given(@"a new external user '(.+)' is added")]
        [When(@"a new external user '(.+)' is added")]
        public void WhenANewExternalUserIsAdded(string userName)
        {
            ExternalUser externalUser = GetExternalUser(userName);
            Try(() => UserService.AddUser(externalUser.accountName, externalUser.accountType, externalUser.displayName, externalUser.memberOfGroups));
        }

        [When(@"information of external user '(.+)' is updated")]
        public void WhenInformationOfExternalUserIsUpdated(string userName)
        {
            ExternalUser externalUser = GetExternalUser(userName);
            Try(() => UserService.UpdateUserGroupsAndRoles(externalUser.accountName, externalUser.accountType, externalUser.memberOfGroups));
        }

        [When(@"account of external user '(.+)' in identity management service is found")]
        public void WhenAccountOfExternalUserInIdentityManagementServiceIsFound(string userName)
        {
            ExternalUser externalUser = GetExternalUser(userName);
            Assert.IsTrue(IdentityManagementService.IsExistingUserAccount(externalUser.accountName, externalUser.accountType));
        }

        [When(@"account of external user '(.+)' in identity management service is not found")]
        public void WhenAccountOfExternalUserInIdentityManagementServiceIsNotFound(string userName)
        {
            ExternalUser externalUser = GetExternalUser(userName);
            Assert.IsFalse(IdentityManagementService.IsExistingUserAccount(externalUser.accountName, externalUser.accountType));
        }

        [When(@"retrieving user information by account name of external user '(.+)'")]
        [Then(@"user information can be retrieved by account name of external user '(.*)'")]
        public void WhenRetrievingUserInformationByAccountNameOfExternalUser(string userName)
        {
            ExternalUser externalUser = GetExternalUser(userName);
            Try(() => UserService.GetUser(externalUser.accountName, externalUser.accountType));
        }

        [Then(@"the user '(.+)' has is a member of the following groups:")]
        public void ThenTheUserHasIsAMemberOfTheFollowingGroups(string user, Table table)
        {
            Guid userId = MockDatabase.GetUser(user).Id;
            List<IGroup> groups = IdentityManagementService.GetUserGroups(userId).ToList();

            Assert.AreEqual(table.Rows.Count, groups.Count);

            foreach (var row in table.Rows)
            {
                Assert.IsNotNull(groups.FirstOrDefault(o => o.Name == row["Group"]), row.ToTableString());
            }
        }

        [Then(@"retrieving user information failed")]
        public void ThenRetrievingUserInformationFailed()
        {
            AssertCaughtException<EntityNotFoundException>();
        }

        private AccountType ResolveAccountType(string type)
        {
            switch (type)
            {
                case "Active directory":
                    return AccountType.ActiveDirectory;
                default:
                    throw new ArgumentException(type);
            }
        }

        private ExternalUser GetExternalUser(string userName)
        {
            return externalUsers.First(o => o.userName == userName);
        }
    }
}