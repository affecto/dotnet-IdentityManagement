using System;
using System.Collections.Generic;
using Affecto.IdentityManagement.Querying.Data;

namespace Affecto.IdentityManagement.Querying
{
    public interface IQueryService
    {
        IReadOnlyCollection<User> GetUsers();
        IReadOnlyCollection<User> GetUsers(string customPropertyName, string customPropertyValue);
        User GetUser(Guid userId);
        User GetUser(string accountName, AccountType accountType);
        bool IsExistingUser(string accountName, AccountType accountType);
        IReadOnlyCollection<Account> GetAccounts(Guid userId);
        IReadOnlyCollection<Group> GetGroups();
        Group GetGroup(Guid groupId);
        IReadOnlyCollection<User> GetGroupMembers(Guid groupId);
        IReadOnlyCollection<Group> GetUserGroups(Guid userId);
        IReadOnlyCollection<Role> GetRoles();
        Role GetRole(Guid roleId);
        IReadOnlyCollection<Permission> GetPermissions();
        IReadOnlyCollection<Organization> GetOrganizations();
        Organization GetOrganization(Guid id);
        string GetPassword(string accountName);
    }
}