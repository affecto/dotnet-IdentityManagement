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
    [Scope(Feature = "Groups")]
    internal class GroupsSteps : StepDefinition
    {
        [Given(@"a group '(.*)' is added")]
        public void GivenAGroupIsAdded(string groupName)
        {
            IGroup group = IdentityManagementService.CreateGroup(groupName, string.Empty, string.Empty);
            nameIdentifierPairs.Add(groupName, group.Id);
        }

        [Given(@"a group '(.+)' is added with a description '(.+)'")]
        [When(@"a group '(.+)' is added with a description '(.+)'")]
        public void WhenAGroupIsAddedWithTheDescription(string groupName, string groupDescription)
        {
            IGroup group = IdentityManagementService.CreateGroup(groupName, groupDescription, null);
            nameIdentifierPairs.Add(groupName, group.Id);
        }

        [Given(@"a user '(.+)'´exists")]
        public void GivenAUserExists(string user)
        {
            MockDatabase.AddUser(user);
        }

        [Given(@"the user '(.+)' is added to the group '(.+)'")]
        [When(@"the user '(.+)' is added to the group '(.+)'")]
        public void WhenTheUserIsAddedToTheGroup(string user, string group)
        {
            Guid userId = MockDatabase.GetUser(user).Id;
            Try(() => IdentityManagementService.AddGroupMember(nameIdentifierPairs[group], userId));
        }

        [When(@"a group '(.+)' is added")]
        public void WhenAGroupIsAdded(string groupName)
        {
            Try(() => GivenAGroupIsAdded(groupName));
        }

        [When(@"the name of the group '(.+)' is changed to '(.+)'")]
        public void WhenTheNameOfTheGroupIsChangedTo(string groupName, string newName)
        {
            Guid groupId = nameIdentifierPairs[groupName];
            IGroup group = IdentityManagementService.GetGroup(groupId);
            Try(() => IdentityManagementService.UpdateGroup(groupId, newName, group.Description, group.IsDisabled, group.ExternalGroupName));
        }

        [When(@"the description of the group '(.+)' is cleared")]
        public void WhenTheDescriptionOfTheGroupIsCleared(string groupName)
        {
            Guid groupId = nameIdentifierPairs[groupName];
            IGroup group = IdentityManagementService.GetGroup(groupId);
            Try(() => IdentityManagementService.UpdateGroup(groupId, groupName, string.Empty, group.IsDisabled, group.ExternalGroupName));
        }

        [When(@"the external group name of the group '(.+)' is cleared")]
        public void WhenTheExternalGroupNameOfTheGroupIsCleared(string groupName)
        {
            Guid groupId = nameIdentifierPairs[groupName];
            IGroup group = IdentityManagementService.GetGroup(groupId);
            Try(() => IdentityManagementService.UpdateGroup(groupId, groupName, group.Description, group.IsDisabled, null));
        }

        [When(@"the group '(.+)' is disabled")]
        public void WhenTheGroupIsDisabled(string group)
        {
            Guid groupId = nameIdentifierPairs[group];
            Try(() => IdentityManagementService.UpdateGroup(groupId, group, string.Empty, true, string.Empty));
        }

        [When(@"the user '(.+)'´is removed from the group '(.+)'")]
        public void WhenTheUserIsRemovedFromTheGroup(string user, string group)
        {
            Guid userId = MockDatabase.GetUser(user).Id;
            Try(() => IdentityManagementService.RemoveGroupMember(nameIdentifierPairs[group], userId));
        }

        [When(@"a group with no name is added")]
        public void WhenAGroupWithNoNameIsAdded()
        {
            Try(() => IdentityManagementService.CreateGroup(string.Empty, string.Empty, string.Empty));
        }

        [When(@"the name of the group '(.+)' is cleared")]
        public void WhenTheNameOfTheGroupIsCleared(string group)
        {
            Guid groupId = nameIdentifierPairs[group];
            Try(() => IdentityManagementService.UpdateGroup(groupId, string.Empty, string.Empty, false, string.Empty));
        }

        [Then(@"the following groups exist:")]
        public void ThenTheFollowingGroupsExist(Table expectedGroups)
        {
            List<IGroup> groups = IdentityManagementService.GetGroups().ToList();
            Assert.AreEqual(expectedGroups.RowCount, groups.Count);
            foreach (TableRow expectedGroup in expectedGroups.Rows)
            {
                Assert.IsTrue(groups.Any(g => g.Name.Equals(expectedGroup["Name"]) && Equals(g.Description, expectedGroup["Description"])
                    && Equals(g.ExternalGroupName, expectedGroup["External group"])), expectedGroup.ToTableString());
            }
        }

        [Then(@"the group '(.+)' is disabled")]
        public void ThenTheGroupIsDisabled(string name)
        {
            IGroup group = IdentityManagementService.GetGroup(nameIdentifierPairs[name]);
            Assert.IsTrue(group.IsDisabled);
        }

        [Then(@"the group '(.+)' has the following members:")]
        public void ThenTheGroupHasTheFollowingMembers(string group, Table expectedMembers)
        {
            List<IUserListItem> members = IdentityManagementService.GetGroupMembers(nameIdentifierPairs[group]).ToList();
            Assert.AreEqual(expectedMembers.RowCount, members.Count);
            foreach (TableRow expectedMember in expectedMembers.Rows)
            {
                Assert.IsTrue(members.Any(m => m.Name.Equals(expectedMember["User"])), expectedMember.ToTableString());
            }
        }

        [Then(@"all groups are enabled")]
        public void ThenAllGroupsAreEnabled()
        {
            IEnumerable<IGroup> groups = IdentityManagementService.GetGroups();
            foreach (IGroup group in groups)
            {
                Assert.IsFalse(group.IsDisabled, group.Name);
            }
        }

        [Then(@"adding the group fails")]
        [Then(@"updating the group fails")]
        public void ThenAddingAGroupFails()
        {
            AssertCaughtException<ArgumentException>();
        }

        [Then(@"adding the group fails because of invalid permissions")]
        [Then(@"updating the group fails because of invalid permissions")]
        [Then("adding the user to the group fails")]
        [Then("removing the user from the group fails")]
        public void ThenAddingTheUserToTheGroupFails()
        {
            AssertCaughtException<InsufficientPermissionsException>();
        }

        [Then(@"operation fails because group with the same name already exists")]
        public void ThenOperationFailsBecauseGroupWithTheSameNameAlreadyExists()
        {
            AssertCaughtException<GroupWithSameNameAlreadyExistsException>();
        }

        private static bool Equals(string value1, string value2)
        {
            if (value1 == null)
            {
                return string.IsNullOrEmpty(value2);
            }
            return value1.Equals(value2);
        }
    }
}