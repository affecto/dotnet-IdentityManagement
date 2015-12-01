using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Affecto.IdentityManagement.Commanding;
using Affecto.IdentityManagement.Querying.Exceptions;
using Affecto.IdentityManagement.Store.EntityFramework.Queries;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework
{
    internal class DbRepository : IDbRepository
    {
        private readonly IDbContext dbContext;

        public DbRepository(IDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            this.dbContext = dbContext;
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public Group GetGroupWithUsers(Guid id)
        {
            GroupWithUsersQuery query = new GroupWithUsersQuery(dbContext.Groups);
            return query.Execute(id);
        }

        public User GetUser(Guid id)
        {
            var query = new UserByIdQuery(dbContext.Users);
            var user = query.Execute(id);

            if (user == null)
            {
                throw new EntityNotFoundException("User", id);
            }

            return user;
        }

        public Account GetUserAccount(Guid userId, AccountType type)
        {
            var query = new AccountQuery(dbContext.Accounts);
            return query.Execute(userId, type);
        }

        public void AddUserAccount(Guid userId, AccountType type, string name, string password = null)
        {
            dbContext.Accounts.Add(new Account { UserId = userId, Type = type, Name = name, Password = password });
        }

        public Permission GetPermission(Guid id)
        {
            var query = new PermissionQuery(dbContext.Permissions);
            return query.Execute(id);
        }

        public User GetUserWithRoles(Guid id)
        {
            UserWithRolesQuery query = new UserWithRolesQuery(dbContext.Users);
            return query.Execute(id);
        }

        public Role GetRole(Guid id)
        {
            RoleQuery query = new RoleQuery(dbContext.Roles);
            return query.Execute(id);
        }

        public Role GetRoleWithPermissions(Guid id)
        {
            RoleWithPermissionsQuery query = new RoleWithPermissionsQuery(dbContext.Roles);
            return query.Execute(id);
        }

        public void AddGroup(Guid id, string name, string description, string externalGroupName)
        {
            dbContext.Groups.Add(new Group { Id = id, Name = name, Description = description, ExternalGroupName = externalGroupName });
        }

        public void AddRole(Guid id, string name, string description, string externalGroupName)
        {
            dbContext.Roles.Add(new Role { Id = id, Name = name, Description = description, ExternalGroupName = externalGroupName });
        }

        public void AddUser(Guid id, string name)
        {
            dbContext.Users.Add(new User { Id = id, Name = name });
        }

        public void AddUser(Guid id, string name, IReadOnlyCollection<KeyValuePair<string, string>> customProperties)
        {
            var user = new User { Id = id, Name = name };

            if (customProperties != null)
            {
                foreach (KeyValuePair<string, string> customProperty in customProperties)
                {
                    user.CustomProperties.Add(new CustomProperty
                    {
                        Id = Guid.NewGuid(),
                        Name = customProperty.Key,
                        Value = customProperty.Value
                    });
                }
            }

            dbContext.Users.Add(user);
        }

        public void RemoveUsers()
        {
            List<User> users = dbContext.Users
                .Include(u => u.Accounts)
                .Include(u => u.CustomProperties)
                .Include(u => u.Groups)
                .Include(u => u.Organizations)
                .Include(u => u.Roles)
                .ToList();

            foreach (User user in users)
            {
                foreach (Account account in user.Accounts.ToList())
                {
                    user.Accounts.Remove(account);
                    dbContext.Accounts.Remove(account);
                }
                foreach (CustomProperty customProperty in user.CustomProperties.ToList())
                {
                    user.CustomProperties.Remove(customProperty);
                    dbContext.CustomProperties.Remove(customProperty);
                }
                foreach (Group @group in user.Groups.ToList())
                {
                    user.Groups.Remove(@group);
                }
                foreach (Organization organization in user.Organizations.ToList())
                {
                    user.Organizations.Remove(organization);
                }
                foreach (Role role in user.Roles.ToList())
                {
                    user.Roles.Remove(role);
                }

                dbContext.Users.Remove(user);
            }
        }

        public void AddOrganization(Guid id, string name, string description)
        {
            dbContext.Organizations.Add(new Organization { Id = id, Name = name, Description = description });
        }

        public Organization GetOrganization(Guid id)
        {
            OrganizationQuery query = new OrganizationQuery(dbContext.Organizations);
            return query.Execute(id);
        }

        public Group GetGroup(Guid id)
        {
            GroupQuery query = new GroupQuery(dbContext.Groups);
            return query.Execute(id);
        }

        public bool AccountExists(AccountType type, string name)
        {
            AccountExistsQuery query = new AccountExistsQuery(dbContext.Accounts);
            return query.Execute(type, name);
        }

        public bool RoleExists(string name)
        {
            var query = new RoleExistsQuery(dbContext.Roles);

            return query.Execute(name);
        }

        public bool GroupExists(string name)
        {
            var query = new GroupExistsQuery(dbContext.Groups);

            return query.Execute(name);
        }

        public bool OrganizationExists(string name)
        {
            var query = new OrganizationExistsQuery(dbContext.Organizations);

            return query.Execute(name);
        }
    }
}