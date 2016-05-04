using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.IdentityManagement.Querying.Data;
using Affecto.IdentityManagement.Querying.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.IdentityManagement.Store.EntityFramework.DatabaseTests.Queries
{
    [TestClass]
    public class FindGroupTests : DbQueryServiceTests
    {
        private Guid groupId1;
        private Guid groupId2;
        private Guid group1User1;
        private Guid groupUser;
        private Guid group2User1;
        private string groupName1;
        private string groupName2;

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void GetGroupWhenThereAreNoGroups()
        {
            sut.GetGroup(Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void GetGroupWhenThereIsNoSuchGroup()
        {
            CreateGroups();

            sut.GetGroup(Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void GetGroupMembersWhenThereAreNoGroups()
        {
            sut.GetGroupMembers(Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void GetGroupMembersWhenThereIsNoSuchGroup()
        {
            CreateGroups();

            sut.GetGroupMembers(Guid.NewGuid());
        }

        [TestMethod]
        public void GetUserGroupsWhenThereAreNoGroups()
        {
            IReadOnlyCollection<Group> groups = sut.GetUserGroups(Guid.NewGuid());

            Assert.IsFalse(groups.Any());
        }

        [TestMethod]
        public void GetUserGroupMembersWhenThereIsNoSuchGroup()
        {
            CreateGroups();

            IReadOnlyCollection<Group> groups = sut.GetUserGroups(Guid.NewGuid());

            Assert.IsFalse(groups.Any());
        }

        [TestMethod]
        public void GetGroupsWhenThereAreNoGroups()
        {
            IReadOnlyCollection<Group> groups = sut.GetGroups();

            Assert.IsFalse(groups.Any());
        }

        [TestMethod]
        public void GetGroupByIdTest()
        {
            CreateGroups();

            Group @group = sut.GetGroup(groupId1);
            
            Assert.AreEqual(groupId1, group.Id);
            Assert.AreEqual(groupName1, group.Name);
        }

        [TestMethod]
        public void GetGroupsTest()
        {
            CreateGroups();
            
            IEnumerable<Group> groups = sut.GetGroups();
            
            Assert.AreEqual(2, groups.Count());
            Assert.IsNotNull(groups.SingleOrDefault(g => g.Id == groupId1));
            Assert.IsNotNull(groups.SingleOrDefault(g => g.Id == groupId2));
        }

        [TestMethod]
        public void GetGroupUsersTest()
        {
            CreateGroups();

            IEnumerable<User> users = sut.GetGroupMembers(groupId1);
            
            Assert.AreEqual(2, users.Count());
            Assert.IsNotNull(users.SingleOrDefault(u => u.Id == group1User1));
            Assert.IsNotNull(users.SingleOrDefault(u => u.Id == groupUser));
        }

        [TestMethod]
        public void GetUserGroupsTest()
        {
            CreateGroups();

            IEnumerable<Group> groups = sut.GetUserGroups(groupUser);
            
            Assert.AreEqual(2, groups.Count());
            Assert.IsNotNull(groups.SingleOrDefault(g => g.Id == groupId1));
            Assert.IsNotNull(groups.SingleOrDefault(g => g.Id == groupId2));
        }

        private void CreateGroups()
        {
            groupId1 = Guid.NewGuid();
            groupName1 = "Developers";
            groupId2 = Guid.NewGuid();
            groupName2 = "Others";
            group1User1 = Guid.NewGuid();
            groupUser = Guid.NewGuid();
            group2User1 = Guid.NewGuid();

            AddGroup(groupId1, groupName1);
            AddGroup(groupId2, groupName2);

            AddActiveDirectoryUser(group1User1, "Name1", "dev\\account1");
            AddActiveDirectoryUser(groupUser, "Name2", "dev\\account2");
            AddActiveDirectoryUser(group2User1, "Name3", "dev\\account3");

            AddUserToGroup(groupId1, groupUser);
            AddUserToGroup(groupId2, groupUser);
            AddUserToGroup(groupId1, group1User1);
            AddUserToGroup(groupId2, group2User1);
        }
    }
}