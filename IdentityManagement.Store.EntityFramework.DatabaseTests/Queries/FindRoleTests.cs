using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.IdentityManagement.Querying.Data;
using Affecto.IdentityManagement.Querying.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.IdentityManagement.Store.EntityFramework.DatabaseTests.Queries
{
    [TestClass]
    public class FindRoleTests : DbQueryServiceTests
    {
        private Guid userId1;
        private Guid userId2;

        private Guid roleId1;
        private Guid roleId2;

        private Guid permissionsId1;
        private Guid permissionsId2;

        private string roleName1 = "Admins";
        private string roleName2 = "Modifiers";

        [TestMethod]
        public void GetRolesWhenThereAreNoRoles()
        {
            IEnumerable<Role> roles = sut.GetRoles();

            Assert.IsFalse(roles.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void GetRoleWhenThereAreNoRoles()
        {
            sut.GetRole(Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void GetRoleWhenThereIsNoSuchRole()
        {
            CreateRoles();

            sut.GetRole(Guid.NewGuid());
        }

        [TestMethod]
        public void GetAllRolesTest()
        {
            CreateRoles();

            IEnumerable<Role> roles = sut.GetRoles();
            
            Assert.AreEqual(2, roles.Count());
            Assert.IsNotNull(roles.SingleOrDefault(r => r.Id == roleId1));
            Assert.IsNotNull(roles.SingleOrDefault(r => r.Id == roleId2));
        }

        [TestMethod]
        public void GetRoleByIdTest()
        {
            CreateRoles();
            
            Role role = sut.GetRole(roleId1);
            
            Assert.AreEqual(roleId1, role.Id);
        }

        private void CreateRoles()
        {
            userId1 = Guid.NewGuid();
            userId2 = Guid.NewGuid();
            roleId1 = Guid.NewGuid();
            roleId2 = Guid.NewGuid();
            permissionsId1 = Guid.NewGuid();
            permissionsId2 = Guid.NewGuid();

            AddActiveDirectoryUser(userId1, "Name1", "dev\\account1");
            AddActiveDirectoryUser(userId2, "Name2", "dev\\account2");
            AddRole(roleId1, roleName1);
            AddRole(roleId2, roleName2);

            AddPermission(permissionsId1, "Edit metadata definitions");
            AddPermission(permissionsId2, "Edit classification");

            AddPermissionToRole(roleId1, permissionsId1);
            AddPermissionToRole(roleId1, permissionsId2);
            AddPermissionToRole(roleId2, permissionsId2);

            AddUserToRole(roleId1, userId1);
            AddUserToRole(roleId2, userId1);
            AddUserToRole(roleId2, userId2);
        }
    }
}