using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.IdentityManagement.Querying.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.IdentityManagement.Store.EntityFramework.DatabaseTests.Queries
{
    [TestClass]
    public class FindUsersByCustomPropertyAndAccountTypeTests : DbQueryServiceTests
    {
        private IReadOnlyCollection<User> result;

        [TestMethod]
        public void NoUsers()
        {
            result = sut.GetUsers("property", "value", AccountType.Password);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void CustomPropertyNameDoesNotMatch()
        {
            const string customPropertyValue = "value";
            var customProperties = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("key", customPropertyValue) };
            AddActiveDirectoryUser(Guid.NewGuid(), "Tim", "timber", false, customProperties);

            result = sut.GetUsers("key2", customPropertyValue, AccountType.ActiveDirectory);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void CustomPropertyValueDoesNotMatch()
        {
            const string customPropertyName = "name";
            var customProperties = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>(customPropertyName, "value1") };
            AddActiveDirectoryUser(Guid.NewGuid(), "Tim", "timber", false, customProperties);

            result = sut.GetUsers(customPropertyName, "value2", AccountType.ActiveDirectory);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void AccountTypeDoesNotMatch()
        {
            const string customPropertyName = "name";
            const string customPropertyValue = "value";
            var customProperties = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>(customPropertyName, customPropertyValue) };
            AddActiveDirectoryUser(Guid.NewGuid(), "Tim", "timber", false, customProperties);

            result = sut.GetUsers(customPropertyName, customPropertyValue, AccountType.Federated);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void AllMatchingUsersAreRetrieved()
        {
            const string customPropertyName = "name";
            const string customPropertyValue = "value";
            var customProperties = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>(customPropertyName, customPropertyValue) };
            AddActiveDirectoryUser(Guid.NewGuid(), "Tim", "timber", false, customProperties);
            AddActiveDirectoryUser(Guid.NewGuid(), "Tom", "tomcat", false, customProperties);
            AddActiveDirectoryUser(Guid.NewGuid(), "John", "johnsnow");

            result = sut.GetUsers(customPropertyName, customPropertyValue, AccountType.ActiveDirectory);

            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(u => u.Name.Equals("Tom")));
            Assert.IsTrue(result.Any(u => u.Name.Equals("Tim")));
        }

        [TestMethod]
        public void AllUserDataIsIncluded()
        {
            const string customPropertyName = "name";
            const string customPropertyValue = "value";
            Guid userId = Guid.NewGuid();
            Guid groupId = Guid.NewGuid();
            Guid roleId = Guid.NewGuid();
            Guid organisationId = Guid.NewGuid();
            var customProperties = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>(customPropertyName, customPropertyValue) };
            AddActiveDirectoryUser(userId, "Tim", "timber", false, customProperties);
            AddGroup(groupId, "group");
            AddRole(roleId, "role");
            AddOrganization(organisationId, "organization");
            AddUserToGroup(groupId, userId);
            AddUserToRole(roleId, userId);
            AddUserToOrganization(organisationId, userId);

            User resultUser = sut.GetUsers(customPropertyName, customPropertyValue, AccountType.ActiveDirectory).Single();

            Assert.AreEqual(groupId, resultUser.Groups.Single().Id);
            Assert.AreEqual(roleId, resultUser.Roles.Single().Id);
            Assert.AreEqual(organisationId, resultUser.Organizations.Single().Id);
            Assert.AreEqual(AccountType.ActiveDirectory, resultUser.Accounts.Single().Type);
        }
    }
}
