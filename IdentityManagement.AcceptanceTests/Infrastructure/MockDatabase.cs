using System;
using System.Linq;
using Affecto.IdentityManagement.AcceptanceTests.Mocking;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.AcceptanceTests.Infrastructure
{
    internal class MockDatabase
    {
        private readonly MockDbContext mockDbContext;

        public MockDatabase(MockDbContext mockDbContext)
        {
            this.mockDbContext = mockDbContext;
        }

        public void AddGroup(string group, string description = null, string externalGroup = null)
        {
            mockDbContext.Groups.Add(new Group { Id = Guid.NewGuid(), Name = group, Description = description, ExternalGroupName = externalGroup });
            mockDbContext.SaveChanges();
        }

        public Group GetGroup(string groupName)
        {
            return mockDbContext.Groups.Single(r => r.Name.Equals(groupName));
        }

        public void AddRole(string role, string description = null, string externalGroup = null)
        {
            mockDbContext.Roles.Add(new Role { Id = Guid.NewGuid(), Name = role, Description = description, ExternalGroupName = externalGroup });
            mockDbContext.SaveChanges();
        }

        public Role GetRole(string role)
        {
            return mockDbContext.Roles.Single(r => r.Name.Equals(role));
        }

        public void AddUser(string user)
        {
            mockDbContext.Users.Add(new User { Id = Guid.NewGuid(), Name = user });
            mockDbContext.SaveChanges();
        }

        public User GetUser(string user)
        {
            return mockDbContext.Users.Single(u => u.Name.Equals(user));
        }

        public void AddOrganization(string organization, bool isDisabled = false)
        {
            mockDbContext.Organizations.Add(new Organization { Id = Guid.NewGuid(), Name = organization, IsDisabled = isDisabled });
            mockDbContext.SaveChanges();
        }

        public Organization GetOrganization(string organization)
        {
            return mockDbContext.Organizations.Single(o => o.Name.Equals(organization));
        }

        public void AddRolePermission(string roleName, string permissionName)
        {
            Role role = GetRole(roleName);
            Permission permission = GetOrCreatePermission(permissionName);
            role.Permissions.Add(permission);
            permission.Roles.Add(role);
            mockDbContext.SaveChanges();
        }

        public void RemoveRolePermission(string roleName, string permissionName)
        {
            Role role = GetRole(roleName);
            Permission permission = GetOrCreatePermission(permissionName);
            role.Permissions.Remove(permission);
            permission.Roles.Remove(role);
            mockDbContext.SaveChanges();
        }

        public Permission GetOrCreatePermission(string permissionName)
        {
            Permission permission = mockDbContext.Permissions.SingleOrDefault(o => o.Name.Equals(permissionName));
            if (permission == null)
            {
                permission = new Permission { Id = Guid.NewGuid(), Name = permissionName, Description = permissionName };
                mockDbContext.Permissions.Add(permission);
                mockDbContext.SaveChanges();
            }
            return permission;
        }
    }
}