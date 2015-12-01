using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.IdentityManagement.Querying;
using Affecto.IdentityManagement.Querying.Data;
using Affecto.IdentityManagement.Querying.Exceptions;
using Affecto.IdentityManagement.Store.EntityFramework.Mapping;
using Affecto.IdentityManagement.Store.EntityFramework.Queries;
using Affecto.Mapping;

namespace Affecto.IdentityManagement.Store.EntityFramework
{
    internal class QueryService : IQueryService
    {
        private readonly IDbContext dbContext;

        public QueryService(IDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentException("dbContext");
            }

            this.dbContext = dbContext;
        }

        public IReadOnlyCollection<User> GetUsers()
        {
            return new UserMapper().Map(dbContext.Users.ToList()).ToList();
        }

        public IReadOnlyCollection<User> GetUsers(string customPropertyName, string customPropertyValue)
        {
            var query = new UsersByCustomPropertyQuery(dbContext.Users);
            IQueryable<Model.User> users = query.Execute(customPropertyName, customPropertyValue);
            
            return new UserMapper().Map(users).ToList();
        }

        public User GetUser(Guid userId)
        {
            var query = new UserByIdQuery(dbContext.Users);
            var user = query.Execute(userId);

            if (user == null)
            {
                throw new EntityNotFoundException("User", userId);
            }

            return new UserMapper().Map(query.Execute(userId));
        }

        public User GetUser(string accountName, AccountType accountType)
        {
            var query = new UserByAccountQuery(dbContext.Users);
            var user = query.Execute(new UserByAccountQuery.UserAccount(accountName, (Model.AccountType)accountType));

            if (user == null)
            {
                throw new EntityNotFoundException("Account", accountName);
            }

            return new UserMapper().Map(user);
        }

        public string GetPassword(string accountName)
        {
            var query = new UserByAccountQuery(dbContext.Users);
            Model.User user = query.Execute(new UserByAccountQuery.UserAccount(accountName, Model.AccountType.Password));

            if (user == null)
            {
                throw new EntityNotFoundException("Account", accountName);
            }

            Model.Account account = user.GetAccount(Model.AccountType.Password, accountName);
            return account.Password;
        }

        public bool IsExistingUser(string accountName, AccountType accountType)
        {
            var query = new UserByAccountQuery(dbContext.Users);
            return query.Execute(new UserByAccountQuery.UserAccount(accountName, (Model.AccountType)accountType)) != null;
        }

        public IReadOnlyCollection<Account> GetAccounts(Guid userId)
        {
            var query = new AccountsQuery(dbContext.Accounts);
            return new AccountMapper().Map(query.Execute(userId)).ToList();
        }

        public IReadOnlyCollection<Group> GetGroups()
        {
            return new GroupMapper().Map(dbContext.Groups).ToList();
        }

        public Group GetGroup(Guid groupId)
        {
            var query = new GroupQuery(dbContext.Groups);
            return new GroupMapper().Map(query.Execute(groupId));
        }

        public IReadOnlyCollection<User> GetGroupMembers(Guid groupId)
        {
            var query = new GroupWithUsersQuery(dbContext.Groups);
            return new UserMapper().Map(query.Execute(groupId).Users).ToList();
        }

        public IReadOnlyCollection<Group> GetUserGroups(Guid userId)
        {
            var query = new UserGroupsQuery(dbContext.Users);
            return new GroupMapper().Map(query.Execute(userId)).ToList();
        }

        public IReadOnlyCollection<Permission> GetPermissions()
        {
            return new PermissionMapper().Map(dbContext.Permissions).ToList();
        }

        public IReadOnlyCollection<Organization> GetOrganizations()
        {
            return new OrganizationMapper().Map(dbContext.Organizations).ToList();
        }

        public Organization GetOrganization(Guid id)
        {
            OrganizationQuery query = new OrganizationQuery(dbContext.Organizations);
            return new OrganizationMapper().Map(query.Execute(id));
        }

        public IReadOnlyCollection<Role> GetRoles()
        {
            var query = new RolesQuery(dbContext.Roles);
            RoleMapper roleMapper = new RoleMapper();
            return roleMapper.Map(query.Execute()).ToList();
        }

        public Role GetRole(Guid roleId)
        {
            RoleWithPermissionsQuery query = new RoleWithPermissionsQuery(dbContext.Roles);
            RoleMapper roleMapper = new RoleMapper();
            return roleMapper.Map(query.Execute(roleId));
        }
    }
}