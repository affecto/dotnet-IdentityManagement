using System;
using System.Linq;
using Affecto.Authentication.Claims;
using Affecto.IdentityManagement.AcceptanceTests.Infrastructure;
using Affecto.IdentityManagement.Interfaces.Exceptions;
using Affecto.IdentityManagement.Interfaces.Model;
using Affecto.IdentityManagement.Store.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Affecto.IdentityManagement.AcceptanceTests.Steps
{
    [Binding]
    internal class RolesSteps : StepDefinition
    {
        [Given(@"a role '(.+)' exists")]
        public void GivenARoleExists(string role)
        {
            MockDatabase.AddRole(role);
        }

        [Given(@"following roles exists:")]
        public void GivenFollowingRolesExists(Table table)
        {
            foreach (var row in table.Rows)
            {
                var name = row["Name"];
                var description = row["Description"];
                var externalGroup = row["External group"];
                MockDatabase.AddRole(name, description, externalGroup);
            }
        }

        [Given(@"role '(.+)' doesn't have '(.+)' permission")]
        public void GivenRoleDoesntHavePermission(string roleName, string permissionName)
        {
            var permission = MockDatabase.GetOrCreatePermission(permissionName);
            var role = MockDatabase.GetRole(roleName);
            if (role.HasPermission(permission.Id))
            {
                MockDatabase.RemoveRolePermission(roleName, permissionName);
            }
        }

        [Given(@"role '(.+)' has permission '(.+)'")]
        public void GivenRoleHasPermission(string roleName, string permissionName)
        {
            if (MockDatabase.GetRole(roleName).Permissions.All(o => o.Name != permissionName))
            {
                MockDatabase.AddRolePermission(roleName, permissionName);
            }
        }

        [When(@"permission '(.+)' is added for role '(.+)'")]
        public void WhenPermissionIsAddedForRole(string permissionName, string roleName)
        {
            Permission permission = MockDatabase.GetOrCreatePermission(permissionName);
            Role role = MockDatabase.GetRole(roleName);

            Try(() => IdentityManagementService.AddRolePermission(role.Id, permission.Id));
        }

        [When(@"permission '(.+)' is removed from role '(.+)'")]
        public void WhenPermissionIsRemovedFromRole(string permissionName, string roleName)
        {
            Permission permission = MockDatabase.GetOrCreatePermission(permissionName);
            Role role = MockDatabase.GetRole(roleName);

            Try(() => IdentityManagementService.RemoveRolePermission(role.Id, permission.Id));
        }

        [When(@"the name of the role '(.+)' is changed to '(.+)'")]
        public void WhenTheNameOfTheRoleIsChangedTo(string roleName, string newName)
        {
            var role = MockDatabase.GetRole(roleName);
            Try(() => IdentityManagementService.UpdateRole(role.Id, newName, role.Description, role.ExternalGroupName));
        }

        [When(@"the description of the role '(.+)' is changed to '(.+)'")]
        public void WhenTheDescriptionOfTheRoleIsChangedTo(string roleName, string description)
        {
            var role = MockDatabase.GetRole(roleName);
            Try(() => IdentityManagementService.UpdateRole(role.Id, role.Name, description, role.ExternalGroupName));
        }

        [Given(@"the external group name of the role '(.+)' is changed to '(.+)'")]
        [When(@"the external group name of the role '(.+)' is changed to '(.+)'")]
        public void WhenTheExternalGroupNameOfTheRoleIsChangedTo(string roleName, string externalGroup)
        {
            var role = MockDatabase.GetRole(roleName);
            Try(() => IdentityManagementService.UpdateRole(role.Id, role.Name, role.Description, externalGroup));
        }

        [When(@"the name of the role '(.+)' is cleared")]
        public void WhenTheNameOfTheRoleIsCleared(string roleName)
        {
            WhenTheNameOfTheRoleIsChangedTo(roleName, null);
        }

        [When(@"the description of the role '(.+)' is cleared")]
        public void WhenTheDescriptionOfTheRoleIsCleared(string roleName)
        {
            WhenTheDescriptionOfTheRoleIsChangedTo(roleName, null);
        }

        [Given(@"the external group name of the role '(.+)' is cleared")]
        [When(@"the external group name of the role '(.+)' is cleared")]
        public void WhenTheExternalGroupNameOfTheRoleIsCleared(string roleName)
        {
            WhenTheExternalGroupNameOfTheRoleIsChangedTo(roleName, null);
        }

        [When(@"a role '(.*)' is added")]
        public void WhenARoleIsAdded(string roleName)
        {
            Try(() => IdentityManagementService.CreateRole(roleName, null, null));
        }

        [Then(@"role '(.+)' has following permissions:")]
        public void ThenRoleHasFollowingPermissions(string roleName, Table table)
        {
            Guid roleId = MockDatabase.GetRole(roleName).Id;
            IRole role = IdentityManagementService.GetRole(roleId);

            foreach (var permission in table.Rows.Select(row => row["Permission"]))
            {
                Assert.IsTrue(role.Permissions.Any(o => o.Name == permission));
            }

            foreach (var permission in role.Permissions)
            {
                Assert.IsTrue(table.Rows.Any(row => row["Permission"] == permission.Name));
            }
        }

        [Then(@"role '(.*)' has no permissions")]
        public void ThenRoleHasNoPermissions(string roleName)
        {
            Guid roleId = MockDatabase.GetRole(roleName).Id;
            IRole role = IdentityManagementService.GetRole(roleId);

            Assert.AreEqual(0, role.Permissions.Count);
        }

        [Then(@"there are following roles:")]
        public void ThenThereAreFollowingRoles(Table table)
        {
            var roles = IdentityManagementService.GetRoles().ToList();

            Assert.AreEqual(table.Rows.Count, roles.Count());

            foreach (var row in table.Rows)
            {
                var name = row["Name"];
                var description = row["Description"];
                var externalGroupName = row["External group"];
                Assert.IsNotNull(
                    roles.SingleOrDefault(o => o.Name == name && Equals(o.Description, description) && Equals(o.ExternalGroupName, externalGroupName)),
                    row.ToTableString());
            }
        }

        [Then(@"adding a new permission for role fails")]
        [Then(@"removing a permission from role fails")]
        [Then(@"updating the role fails because of invalid permissions")]
        public void ThenAddingANewPermissionForRoleFails()
        {
            AssertCaughtException<InsufficientPermissionsException>();
        }

        [Then(@"updating the role fails")]
        public void ThenUpdatingTheRoleFails()
        {
            AssertCaughtException<ArgumentException>();
        }

        [Then(@"operation fails because a role with the same name already exists")]
        public void ThenOperationFailsBecauseARoleWithTheSameNameAlreadyExists()
        {
            AssertCaughtException<RoleWithSameNameAlreadyExistsException>();
        }

        private static bool Equals(string value1, string value2)
        {
            if (value1 == null)
            {
                return string.IsNullOrEmpty(value2);
            }
            return value1.Equals(value2);
        }
    }
}