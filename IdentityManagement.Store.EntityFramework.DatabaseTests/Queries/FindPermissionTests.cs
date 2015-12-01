using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.IdentityManagement.Querying.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.IdentityManagement.Store.EntityFramework.DatabaseTests.Queries
{
    [TestClass]
    public class FindPermissionTests : DbQueryServiceTests
    {
        [TestMethod]
        public void GetPermissionsTest()
        {
            Guid permission1Id = Guid.NewGuid();
            Guid permission2Id = Guid.NewGuid();

            AddPermission(permission1Id, "Modify");
            AddPermission(permission2Id, "Read");

            IReadOnlyCollection<Permission> permissions = sut.GetPermissions();
            Assert.AreEqual(2, permissions.Count());
            Assert.IsNotNull(permissions.SingleOrDefault(p => p.Id == permission1Id));
            Assert.IsNotNull(permissions.SingleOrDefault(p => p.Id == permission2Id));
        }

        [TestMethod]
        public void GetPermissionsWhenThereAreNoPermissions()
        {
            IReadOnlyCollection<Permission> permissions = sut.GetPermissions();
            
            Assert.IsFalse(permissions.Any());
        }
    }
}