using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using Affecto.IdentityManagement.Store.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.IdentityManagement.Store.EntityFramework.DatabaseTests.Commands
{
    [TestClass]
    public class AddUserTests : DbRepositoryTests
    {
        [TestMethod]
        public void AddUser()
        {
            Guid userId = Guid.NewGuid();
            const string userName = "user";

            sut.AddUser(userId, userName);
            sut.SaveChanges();

            using (DbContext readContext = new DbContext())
            {
                User user = readContext.Users.Single();

                Assert.AreEqual(userId, user.Id);
                Assert.AreEqual(userName, user.Name);
            }
        }

        [TestMethod]
        public void AddUserWithCustomProperties()
        {
            Guid userId = Guid.NewGuid();
            const string userName = "user";

            const string emailName = "email";
            const string emailValue = "foo@test.com";
            const string addressName = "address";
            const string addressValue = "street 123";

            var customProperties = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(emailName,  emailValue),
                new KeyValuePair<string, string>(addressName, addressValue)
            };

            sut.AddUser(userId, userName, customProperties);
            sut.SaveChanges();

            using (DbContext readContext = new DbContext())
            {
                User user = readContext.Users.Include(u => u.CustomProperties).Single();

                Assert.AreEqual(userId, user.Id);
                Assert.AreEqual(userName, user.Name);

                Assert.AreEqual(2, user.CustomProperties.Count);
                Assert.IsNotNull(user.CustomProperties.SingleOrDefault(c => c.Name == emailName && c.Value == emailValue));
                Assert.IsNotNull(user.CustomProperties.SingleOrDefault(c => c.Name == addressName && c.Value == addressValue));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddUserWithNoName()
        {
            sut.AddUser(Guid.NewGuid(), null);
            dbContext.SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void AddTwoUsersWithSameId()
        {
            Guid userId = Guid.NewGuid();

            sut.AddUser(userId, "name");
            sut.AddUser(userId, "another name");
            sut.SaveChanges();
        }
    }
}