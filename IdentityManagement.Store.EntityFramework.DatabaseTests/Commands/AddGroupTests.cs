using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using Affecto.IdentityManagement.Store.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.IdentityManagement.Store.EntityFramework.DatabaseTests.Commands
{
    [TestClass]
    public class AddGroupTests : DbRepositoryTests
    {
        private string externalGroupName = "AD group name";

        [TestMethod]
        public void AddGroup()
        {
            Guid groupId = Guid.NewGuid();
            const string groupName = "users";
            const string groupDescription = "some group";

            sut.AddGroup(groupId, groupName, groupDescription, externalGroupName);
            sut.SaveChanges();

            using (DbContext readContext = new DbContext())
            {
                Group group = readContext.Groups.Single();

                Assert.AreEqual(groupId, group.Id);
                Assert.AreEqual(groupName, group.Name);
                Assert.AreEqual(groupDescription, group.Description);
                Assert.AreEqual(externalGroupName, group.ExternalGroupName);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddGroupWithNoName()
        {
            sut.AddGroup(Guid.NewGuid(), null, "description", externalGroupName);
            dbContext.SaveChanges();
        }

        [TestMethod]
        public void AddGroupWithNoDescription()
        {
            sut.AddGroup(Guid.NewGuid(), "users", null, externalGroupName);
            sut.SaveChanges();

            using (DbContext readContext = new DbContext())
            {
                Group group = readContext.Groups.Single();

                Assert.IsNull(group.Description);
            }
        }

        [TestMethod]
        public void AddGroupWithNoExternalGroupName()
        {
            sut.AddGroup(Guid.NewGuid(), "users", "description", null);
            sut.SaveChanges();

            using (DbContext readContext = new DbContext())
            {
                Group group = readContext.Groups.Single();

                Assert.IsNull(group.ExternalGroupName);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void AddTwoGroupsWithSameId()
        {
            Guid groupId = Guid.NewGuid();

            sut.AddGroup(groupId, "name", null, externalGroupName);
            sut.AddGroup(groupId, "another name", null, externalGroupName);
            sut.SaveChanges();
        }
    }
}