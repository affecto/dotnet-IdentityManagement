using System;
using System.Collections.Generic;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Commanding
{
    public interface IDbRepository
    {
        void SaveChanges();
        Group GetGroupWithUsers(Guid id);
        User GetUser(Guid id);
        User GetUserWithRoles(Guid id);
        Role GetRole(Guid id);
        Role GetRoleWithPermissions(Guid id);
        void AddGroup(Guid id, string name, string description, string externalGroupName);
        void AddRole(Guid id, string name, string description, string externalGroupName);
        void AddUser(Guid id, string name);
        void AddOrganization(Guid id, string name, string description);
        Organization GetOrganization(Guid id);
        Group GetGroup(Guid id);
        Account GetUserAccount(Guid userId, AccountType type);
        void AddUserAccount(Guid userId, AccountType type, string name, string password = null);
        Permission GetPermission(Guid id);
        bool AccountExists(AccountType type, string name);
        bool RoleExists(string name);
        bool GroupExists(string name);
        bool OrganizationExists(string name);
        void AddUser(Guid id, string name, IReadOnlyCollection<KeyValuePair<string, string>> customProperties);
        void RemoveUsers();
    }
}