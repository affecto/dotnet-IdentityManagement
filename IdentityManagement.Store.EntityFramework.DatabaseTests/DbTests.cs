using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.IdentityManagement.Store.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.IdentityManagement.Store.EntityFramework.DatabaseTests
{
    [TestClass]
    [DeploymentItem(@"..\packages\EntityFramework.6.1.3\lib\net45\EntityFramework.dll")]
    [DeploymentItem(@"..\packages\EntityFramework.6.1.3\lib\net45\EntityFramework.SqlServer.dll")]
    [DeploymentItem(@"..\packages\EntityFramework.SqlServerCompact.6.1.3\lib\net45\EntityFramework.SqlServerCompact.dll")]
    [DeploymentItem(@"..\packages\Microsoft.SqlServer.Compact.4.0.8876.1\lib\net40\System.Data.SqlServerCe.dll")]
    [DeploymentItem(@"..\packages\Microsoft.SqlServer.Compact.4.0.8876.1\lib\net40\System.Data.SqlServerCe.Entity.dll")]
    public abstract class DbTests
    {
        internal DbContext dbContext;

        [TestInitialize]
        public void CreateDbContext()
        {
            dbContext = new DbContext();
        }

        [TestCleanup]
        public void DeleteDatabase()
        {
            dbContext.Database.Delete();
            dbContext.Dispose();
        }

        protected static void AddUser(Guid userId, string name, string accountName, bool isDisabled = false,
            IEnumerable<KeyValuePair<string, string>> customProperties = null)
        {
            using (DbContext writeDbContext = new DbContext())
            {
                User user = new User
                {
                    Id = userId,
                    Name = name,
                    IsDisabled = isDisabled,
                };

                Account account = new Account
                {
                    UserId = user.Id,
                    Name = accountName,
                    Type = AccountType.ActiveDirectory
                };

                user.Accounts.Add(account);

                if (customProperties != null)
                {
                    user.CustomProperties.AddRange(customProperties.Select(c => new CustomProperty { Id = Guid.NewGuid(), Name = c.Key, Value = c.Value }));
                }

                writeDbContext.Users.Add(user);
                writeDbContext.SaveChanges();
            }
        }

        protected static void AddGroup(Guid groupId, string name)
        {
            using (DbContext writeDbContext = new DbContext())
            {
                Group group = new Group
                {
                    Id = groupId,
                    Name = name,
                };

                writeDbContext.Groups.Add(group);
                writeDbContext.SaveChanges();
            }
        }

        protected static void AddUserToGroup(Guid groupId, Guid userId)
        {
            using (DbContext writeDbContext = new DbContext())
            {
                User user = writeDbContext.Users.Single(u => u.Id == userId);
                Group group = writeDbContext.Groups.Single(g => g.Id == groupId);
                group.Users.Add(user);
                user.Groups.Add(group);

                writeDbContext.SaveChanges();
            }
        }

        protected static void AddRole(Guid roleId, string roleName)
        {
            using (DbContext writeDbContext = new DbContext())
            {
                Role role = new Role
                {
                    Id = roleId,
                    Name = roleName
                };

                writeDbContext.Roles.Add(role);
                writeDbContext.SaveChanges();
            }
        }

        protected static void AddUserToRole(Guid roleId, Guid userId)
        {
            using (DbContext writeDbContext = new DbContext())
            {
                User user = writeDbContext.Users.Single(u => u.Id == userId);
                Role role = writeDbContext.Roles.Single(r => r.Id == roleId);
                user.Roles.Add(role);

                writeDbContext.SaveChanges();
            }
        }

        protected static void AddPermission(Guid permissionId, string name)
        {
            using (DbContext writeDbContext = new DbContext())
            {
                Permission permission = new Permission
                {
                    Id = permissionId,
                    Name = name
                };
                writeDbContext.Permissions.Add(permission);
                writeDbContext.SaveChanges();
            }
        }

        protected static void AddPermissionToRole(Guid roleId, Guid permissionId)
        {
            using (DbContext writeDbContext = new DbContext())
            {
                Role role = writeDbContext.Roles.Single(r => r.Id == roleId);
                Permission permission = writeDbContext.Permissions.Single(p => p.Id == permissionId);
                role.Permissions.Add(permission);
                writeDbContext.SaveChanges();
            }
        }

        protected static void AddOrganization(Guid id, string name, string description = null, bool isDisabled = false)
        {
            using (DbContext writeDbContext = new DbContext())
            {
                Organization organization = new Organization
                {
                    Id = id,
                    Name = name,
                    Description = description,
                    IsDisabled = isDisabled
                };
                writeDbContext.Organizations.Add(organization);
                writeDbContext.SaveChanges();
            }
        }

        protected static void AddUserToOrganization(Guid userId, Guid organizationId)
        {
            using (DbContext writeDbContext = new DbContext())
            {
                User user = writeDbContext.Users.Single(u => u.Id == userId);
                Organization organization = writeDbContext.Organizations.Single(r => r.Id == organizationId);
                user.Organizations.Add(organization);

                writeDbContext.SaveChanges();
            }
        }
    }
}