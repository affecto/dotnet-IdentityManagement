using System;
using System.Collections.Generic;
using Affecto.IdentityManagement.Interfaces.Model;

namespace Affecto.IdentityManagement.Interfaces
{
    public interface IIdentityManagementService
    {
        IEnumerable<IUserListItem> GetUsers();
        IEnumerable<IUser> GetUsers(string customPropertyName, string customPropertyValue);
        IEnumerable<IUser> GetUsers(string customPropertyName, string customPropertyValue, AccountType accountType);
        IUser GetUser(Guid userId);
        IUserListItem CreateUser(string name);
        void UpdateUser(Guid id, string name, bool isDisabled);
        void AddUserAccount(Guid userId, string name, string password);
        void AddExternalUserAccount(Guid userId, AccountType type, string name);
        IEnumerable<IAccount> GetUserAccounts(Guid userId);
        void UpdateExternalUserAccount(Guid userId, AccountType type, string name);
        void RemoveUserAccount(Guid userId, AccountType type, string name);
        bool IsExistingUserAccount(string accountName, AccountType type);
        void AddUserOrganization(Guid userId, Guid organizationId);
        void RemoveUserOrganization(Guid userId, Guid organizationId);

        IEnumerable<IGroup> GetGroups();
        IGroup GetGroup(Guid groupId);
        IGroup CreateGroup(string name, string description, string externalGroupName);
        IEnumerable<IUserListItem> GetGroupMembers(Guid groupId);
        IUserListItem AddGroupMember(Guid groupId, Guid userId);
        IUserListItem RemoveGroupMember(Guid groupId, Guid userId);
        IEnumerable<IGroup> GetUserGroups(Guid userId);
        void UpdateGroup(Guid id, string name, string description, bool isDisabled, string externalGroupName);

        IEnumerable<IRole> GetRoles();
        IRole GetRole(Guid roleId);
        IRole CreateRole(string name, string description, string externalGroupName);
        void UpdateRole(Guid id, string name, string description, string externalGroupName);
        void AddUserRole(Guid userId, Guid roleId);
        void RemoveUserRole(Guid userId, Guid roleId);
        IEnumerable<IPermission> GetPermissions();
        void AddRolePermission(Guid roleId, Guid permissionId);
        void RemoveRolePermission(Guid roleId, Guid permissionId);

        IEnumerable<IOrganization> GetOrganizations();
        IOrganization GetOrganization(Guid id);
        void UpdateOrganization(Guid id, string name, string description, string email, bool isDisabled);
        IOrganization CreateOrganization(string name, string description);
        IUserListItem CreateUser(string name, IEnumerable<KeyValuePair<string, string>> customProperties);
    }
}