using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using Affecto.IdentityManagement.Store.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.IdentityManagement.Store.EntityFramework.DatabaseTests.Commands
{
    [TestClass]
    public class AddOrganizationTests : DbRepositoryTests
    {
        [TestMethod]
        public void AddOrganization()
        {
            Guid organizationId = Guid.NewGuid();
            const string organizationName = "development";
            const string organizationDescription = "some people";

            sut.AddOrganization(organizationId, organizationName, organizationDescription);
            sut.SaveChanges();

            using (DbContext readContext = new DbContext())
            {
                Organization organization = readContext.Organizations.Single();

                Assert.AreEqual(organizationId, organization.Id);
                Assert.AreEqual(organizationName, organization.Name);
                Assert.AreEqual(organizationDescription, organization.Description);
                Assert.AreEqual(null, organization.Email);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddOrganizationWithNoName()
        {
            sut.AddOrganization(Guid.NewGuid(), null, "description");
            dbContext.SaveChanges();
        }

        [TestMethod]
        public void AddOrganizationWithNoDescription()
        {
            sut.AddOrganization(Guid.NewGuid(), "users", null);
            sut.SaveChanges();

            using (DbContext readContext = new DbContext())
            {
                Organization organization = readContext.Organizations.Single();

                Assert.IsNull(organization.Description);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void AddTwoOrganizationsWithSameId()
        {
            Guid organizationId = Guid.NewGuid();

            sut.AddOrganization(organizationId, "name", null);
            sut.AddOrganization(organizationId, "another name", null);
            sut.SaveChanges();
        }

        [TestMethod]
        public void AddOrganizationWithNoEmail()
        {
            sut.AddOrganization(Guid.NewGuid(), "users", "description");
            sut.SaveChanges();

            using (DbContext readContext = new DbContext())
            {
                Organization organization = readContext.Organizations.Single();

                Assert.IsNull(organization.Email);
            }
        }
    }
}