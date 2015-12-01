using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.IdentityManagement.Querying.Data;
using Affecto.IdentityManagement.Querying.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.IdentityManagement.Store.EntityFramework.DatabaseTests.Queries
{
    [TestClass]
    public class FindOrganizationTests : DbQueryServiceTests
    {
        [TestMethod]
        public void GetOrganizationsWhenThereAreNoOrganizations()
        {
            IReadOnlyCollection<Organization> organizations = sut.GetOrganizations();

            Assert.IsFalse(organizations.Any());
        }

        [TestMethod]
        public void GetOrganizations()
        {
            Guid organizationId1 = Guid.NewGuid();
            Guid organizationId2 = Guid.NewGuid();
            const string organizationName1 = "org1";
            const string organizationName2 = "org2";
            const string organizationDescription1 = "desc";

            AddOrganization(organizationId1, organizationName1, organizationDescription1, false);
            AddOrganization(organizationId2, organizationName2, string.Empty, true);

            IReadOnlyCollection<Organization> organizations = sut.GetOrganizations();

            Assert.AreEqual(2, organizations.Count);
            Assert.IsTrue(organizations.Any(o => o.Id.Equals(organizationId1) && o.Name.Equals(organizationName1) && o.Description.Equals(organizationDescription1) && !o.IsDisabled));
            Assert.IsTrue(organizations.Any(o => o.Id.Equals(organizationId2) && o.Name.Equals(organizationName2) && o.Description.Equals(string.Empty) && o.IsDisabled));
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void GetOrganizationWhenThereAreNoOrganizations()
        {
            sut.GetOrganization(Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void GetOrganizationWhenThereIsNoSuchOrganization()
        {
            AddOrganization(Guid.NewGuid(), "name");

            sut.GetOrganization(Guid.NewGuid());
        }

        [TestMethod]
        public void GetOrganization()
        {
            Guid organizationId = Guid.NewGuid();
            const string organizationName = "org";
            const string organizationDescription = "desc";
            const bool isDisabled = true;

            AddOrganization(organizationId, organizationName, organizationDescription, isDisabled);

            Organization organization = sut.GetOrganization(organizationId);

            Assert.AreEqual(organizationId, organization.Id);
            Assert.AreEqual(organizationName, organization.Name);
            Assert.AreEqual(organizationDescription, organization.Description);
            Assert.AreEqual(isDisabled, organization.IsDisabled);
        }
    }
}