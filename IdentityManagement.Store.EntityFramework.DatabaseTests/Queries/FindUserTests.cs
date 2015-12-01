using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.IdentityManagement.Querying.Data;
using Affecto.IdentityManagement.Querying.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.IdentityManagement.Store.EntityFramework.DatabaseTests.Queries
{
    [TestClass]
    public class FindUserTests : DbQueryServiceTests
    {
        private Guid userId1;
        private Guid userId2;
        private string name1;
        private string name2;
        private string accountName1;
        private string accountName2;
        private List<KeyValuePair<string, string>> user1CustomProperties;
            
        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void GetUserByIdWhenThereAreNoUsers()
        {
            sut.GetUser(Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void GetUserByIdWhenThereIsNoSuchUser()
        {
            CreateUsers();

            sut.GetUser(Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void GetUserByAccountWhenThereAreNoUsers()
        {
            sut.GetUser("account", AccountType.ActiveDirectory);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void GetUserByAccountWhenThereIsNoSuchUserWithAccountName()
        {
            CreateUsers();

            sut.GetUser("account", AccountType.ActiveDirectory);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void GetUserByAccountWhenThereIsNoSuchUserWithAccountType()
        {
            CreateUsers();

            sut.GetUser("DEV\\JOHNMIK", AccountType.Federated);
        }

        [TestMethod]
        public void ThereIsUserWithAccountName()
        {
            CreateUsers();

            var userExists = sut.IsExistingUser("DEV\\JOHNMIK", AccountType.ActiveDirectory);

            Assert.IsTrue(userExists);
        }

        [TestMethod]
        public void UserDoesNotExists()
        {
            CreateUsers();

            var userExists = sut.IsExistingUser("account", AccountType.ActiveDirectory);

            Assert.IsFalse(userExists);
        }

        [TestMethod]
        public void GetUsersWhenThereAreNoUsers()
        {
            IReadOnlyCollection<User> users = sut.GetUsers();

            Assert.IsFalse(users.Any());
        }

        [TestMethod]
        public void GetUserById()
        {
            CreateUsers();

            User user = sut.GetUser(userId1);

            Assert.AreEqual(name1, user.Name);
            Assert.IsFalse(user.IsDisabled);
            Assert.IsNotNull(user.Accounts.SingleOrDefault(u => u.Name == accountName1));
        }

        [TestMethod]
        public void GetUserByAccountName()
        {
            CreateUsers();

            User user = sut.GetUser("DEV\\JOHNMIK", AccountType.ActiveDirectory);

            Assert.AreEqual(name1, user.Name);
            Assert.IsFalse(user.IsDisabled);
            Assert.IsNotNull(user.Accounts.SingleOrDefault(u => u.Name == accountName1));
        }

        [TestMethod]
        public void GetAllUsers()
        {
            CreateUsers();

            IEnumerable<User> users = sut.GetUsers();

            Assert.AreEqual(2, users.Count());
            Assert.IsNotNull(users.SingleOrDefault(u => u.Name == name1));
            Assert.IsNotNull(users.SingleOrDefault(u => u.Name == name2));
        }

        [TestMethod]
        public void GetUserOrganizationsById()
        {
            CreateUsers();

            User user = sut.GetUser(userId1);
            Assert.AreEqual(1, user.Organizations.Count);
            Assert.IsNotNull(user.Organizations.SingleOrDefault(o => o.Name == "R&D"));
        }

        [TestMethod]
        public void GetUserGroupsById()
        {
            CreateUsers();

            User user = sut.GetUser(userId2);
            Assert.AreEqual(1, user.Groups.Count);
            Assert.IsNotNull(user.Groups.SingleOrDefault(o => o.Name == "Developers"));
        }

        [TestMethod]
        public void GetUserRolesById()
        {
            CreateUsers();

            User user = sut.GetUser(userId1);
            Assert.AreEqual(1, user.Roles.Count);
            Assert.IsNotNull(user.Roles.Where(o => o.Name == "Admin"));
            Assert.AreEqual(1, user.Roles.Single().Permissions.Count);
            Assert.IsTrue(user.Roles.Single().Permissions.Any(p => p.Name == "Modify Classification"));
        }

        [TestMethod]
        public void GetUserCustomPropertiesById()
        {
            CreateUsers();

            User user = sut.GetUser(userId1);
            Assert.AreEqual(2, user.CustomProperties.Count);
            Assert.IsNotNull(user.CustomProperties.SingleOrDefault(c => c.Name == "email" && c.Value == "mike@johnsson.com"));
            Assert.IsNotNull(user.CustomProperties.SingleOrDefault(c => c.Name == "address" && c.Value == null));
        }

        [TestMethod]
        public void GetUserOrganizationsByAccountName()
        {
            CreateUsers();

            User user = sut.GetUser("DEV\\JOHNMIK", AccountType.ActiveDirectory);
            Assert.AreEqual(1, user.Organizations.Count);
            Assert.IsNotNull(user.Organizations.SingleOrDefault(o => o.Name == "R&D"));
        }

        [TestMethod]
        public void GetUserGroupsByAccountName()
        {
            CreateUsers();

            User user = sut.GetUser("dev\\johnjoh", AccountType.ActiveDirectory);
            Assert.AreEqual(1, user.Groups.Count);
            Assert.IsNotNull(user.Groups.SingleOrDefault(o => o.Name == "Developers"));
        }

        [TestMethod]
        public void GetUserRolesByAccountName()
        {
            CreateUsers();

            User user = sut.GetUser("DEV\\JOHNMIK", AccountType.ActiveDirectory);
            Assert.AreEqual(1, user.Roles.Count);
            Assert.IsNotNull(user.Roles.SingleOrDefault(o => o.Name == "Admin"));
            Assert.AreEqual(1, user.Roles.Single().Permissions.Count);
            Assert.IsTrue(user.Roles.Single().Permissions.Any(p => p.Name == "Modify Classification"));
        }

        [TestMethod]
        public void GetUserCustomPropertiesByAccountName()
        {
            CreateUsers();

            User user = sut.GetUser("DEV\\JOHNMIK", AccountType.ActiveDirectory);
            Assert.AreEqual(2, user.CustomProperties.Count);
            Assert.IsNotNull(user.CustomProperties.SingleOrDefault(c => c.Name == "email" && c.Value == "mike@johnsson.com"));
            Assert.IsNotNull(user.CustomProperties.SingleOrDefault(c => c.Name == "address" && c.Value == null));
        }

        private void CreateUsers()
        {
            userId1 = Guid.NewGuid();
            name1 = "Mike Johnsson";
            accountName1 = "dev\\johnmik";
            user1CustomProperties = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("email", "mike@johnsson.com"),
                new KeyValuePair<string, string>("address", null)
            };

            userId2 = Guid.NewGuid();
            name2 = "John Johnsson";
            accountName2 = "dev\\johnjoh";

            Guid organizationId = Guid.NewGuid();
            Guid groupId = Guid.NewGuid();
            Guid roleId = Guid.NewGuid();
            Guid permissionId = Guid.NewGuid();

            AddUser(userId1, name1, accountName1, false, user1CustomProperties);
            AddUser(userId2, name2, accountName2);

            AddOrganization(organizationId, "R&D");
            AddUserToOrganization(userId1, organizationId);

            AddGroup(groupId, "Developers");
            AddUserToGroup(groupId, userId2);

            AddRole(roleId, "Admin");
            AddPermission(permissionId, "Modify Classification");
            AddPermissionToRole(roleId, permissionId);
            AddUserToRole(roleId, userId1);
        }
    }
}