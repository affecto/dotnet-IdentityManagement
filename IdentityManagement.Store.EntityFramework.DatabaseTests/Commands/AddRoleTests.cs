using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using Affecto.IdentityManagement.Store.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.IdentityManagement.Store.EntityFramework.DatabaseTests.Commands
{
    [TestClass]
    public class AddRoleTests : DbRepositoryTests
    {
        private string groupName = "AD group name";
        private string description = "Description";

        [TestMethod]
        public void AddRole()
        {
            Guid roleId = Guid.NewGuid();
            const string roleName = "users";

            sut.AddRole(roleId, roleName, description, groupName);
            sut.SaveChanges();

            using (DbContext readContext = new DbContext())
            {
                Role role = readContext.Roles.Single();

                Assert.AreEqual(roleId, role.Id);
                Assert.AreEqual(roleName, role.Name);
                Assert.AreEqual(groupName, role.ExternalGroupName);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddRoleWithNoName()
        {
            sut.AddRole(Guid.NewGuid(), null, description, groupName);
            dbContext.SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void AddTwoRolesWithSameId()
        {
            Guid roleId = Guid.NewGuid();

            sut.AddRole(roleId, "name", description, groupName);
            sut.AddRole(roleId, "another name", description, groupName);
            sut.SaveChanges();
        }
    }
}