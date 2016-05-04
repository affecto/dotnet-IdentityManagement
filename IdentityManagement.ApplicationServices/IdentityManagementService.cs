using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.Authentication.Claims;
using Affecto.IdentityManagement.ApplicationServices.Mapping;
using Affecto.IdentityManagement.Commanding.Commands;
using Affecto.IdentityManagement.Interfaces;
using Affecto.IdentityManagement.Interfaces.Model;
using Affecto.IdentityManagement.Querying;
using Affecto.Mapping;
using Affecto.Patterns.Cqrs;
using AccountType = Affecto.IdentityManagement.Interfaces.Model.AccountType;

namespace Affecto.IdentityManagement.ApplicationServices
{
    internal class IdentityManagementService : IIdentityManagementService
    {
        private readonly IQueryService queryService;
        private readonly Lazy<ICommandBus> commandBus;
        private readonly IAuthenticatedUserContext userContext;

        public IdentityManagementService(IQueryService queryService, Lazy<ICommandBus> commandBus, IAuthenticatedUserContext userContext)
        {
            if (queryService == null)
            {
                throw new ArgumentException("queryService");
            }
            if (commandBus == null)
            {
                throw new ArgumentNullException("commandBus");
            }
            if (userContext == null)
            {
                throw new ArgumentNullException("userContext");
            }

            this.queryService = queryService;
            this.commandBus = commandBus;
            this.userContext = userContext;
        }

        public IEnumerable<IUserListItem> GetUsers()
        {
            var mapper = new UserListItemMapper();
            return mapper.Map(queryService.GetUsers());
        }

        public IEnumerable<IUser> GetUsers(string customPropertyName, string customPropertyValue)
        {
            var mapper = new UserMapper();
            return mapper.Map(queryService.GetUsers(customPropertyName, customPropertyValue));
        }

        public IEnumerable<IUser> GetUsers(string customPropertyName, string customPropertyValue, AccountType accountType)
        {
            var mapper = new UserMapper();
            return mapper.Map(queryService.GetUsers(customPropertyName, customPropertyValue, (Querying.Data.AccountType)accountType));
        }

        public IUser GetUser(Guid userId)
        {
            var mapper = new UserMapper();
            return mapper.Map(queryService.GetUser(userId));
        }

        public IUserListItem CreateUser(string name)
        {
            return CreateUser(name, null);
        }

        public IUserListItem CreateUser(string name, IEnumerable<KeyValuePair<string, string>> customProperties)
        {
            userContext.CheckPermission(Permissions.UserMaintenance);

            var properties = customProperties != null ? customProperties.ToList() : new List<KeyValuePair<string, string>>(0);

            Guid id = Guid.NewGuid();
            var command = new CreateUserCommand(id, name, properties);
            commandBus.Value.Send(Envelope.Create(command));
            return GetUser(id);
        }

        public void UpdateUser(Guid id, string name, bool isDisabled)
        {
            userContext.CheckPermission(Permissions.UserMaintenance);

            var command = new UpdateUserCommand(id, name, isDisabled);
            commandBus.Value.Send(Envelope.Create(command));
        }

        public void AddUserAccount(Guid userId, string name, string password)
        {
            userContext.CheckPermission(Permissions.UserMaintenance);

            var command = new CreateUserPasswordAccountCommand(userId, name, password);
            commandBus.Value.Send(Envelope.Create(command));
        }

        public void AddExternalUserAccount(Guid userId, AccountType type, string name)
        {
            userContext.CheckPermission(Permissions.UserMaintenance);

            var command = new CreateExternalUserAccountCommand(userId, type, name);
            commandBus.Value.Send(Envelope.Create(command));
        }

        public IEnumerable<IAccount> GetUserAccounts(Guid userId)
        {
            var mapper = new UserAccountMapper();
            return mapper.Map(queryService.GetAccounts(userId));
        }

        public void UpdateExternalUserAccount(Guid userId, AccountType type, string name)
        {
            userContext.CheckPermission(Permissions.UserMaintenance);

            var command = new UpdateUserAccountCommand(userId, type, name);
            commandBus.Value.Send(Envelope.Create(command));
        }

        public void RemoveUserAccount(Guid userId, AccountType type, string name)
        {
            userContext.CheckPermission(Permissions.UserMaintenance);

            var command = new RemoveUserAccountCommand(userId, type, name);
            commandBus.Value.Send(Envelope.Create(command));
        }

        public bool IsExistingUserAccount(string accountName, AccountType type)
        {
            userContext.CheckPermission(Permissions.UserMaintenance);

            return queryService.IsExistingUser(accountName, (Querying.Data.AccountType) type);
        }

        public void RemoveUserOrganization(Guid userId, Guid organizationId)
        {
            userContext.CheckPermission(Permissions.UserMaintenance);

            ICommand command = new RemoveUserOrganizationCommand(userId, organizationId);
            commandBus.Value.Send(Envelope.Create(command));
        }

        public void AddUserOrganization(Guid userId, Guid organizationId)
        {
            userContext.CheckPermission(Permissions.UserMaintenance);

            ICommand command = new AddUserOrganizationCommand(userId, organizationId);
            commandBus.Value.Send(Envelope.Create(command));
        }

        public IEnumerable<IGroup> GetGroups()
        {
            var mapper = new GroupMapper();
            return mapper.Map(queryService.GetGroups());
        }

        public IGroup GetGroup(Guid groupId)
        {
            var mapper = new GroupMapper();
            return mapper.Map(queryService.GetGroup(groupId));
        }

        public IGroup CreateGroup(string name, string description, string externalGroupName)
        {
            userContext.CheckPermission(Permissions.UserMaintenance);

            Guid id = Guid.NewGuid();
            var command = new CreateGroupCommand(id, name, description, externalGroupName);
            commandBus.Value.Send(Envelope.Create(command));
            return GetGroup(id);
        }

        public IEnumerable<IUserListItem> GetGroupMembers(Guid groupId)
        {
            var mapper = new UserListItemMapper();
            return mapper.Map(queryService.GetGroupMembers(groupId));
        }

        public IUserListItem AddGroupMember(Guid groupId, Guid userId)
        {
            userContext.CheckPermission(Permissions.UserMaintenance);

            var command = new AddGroupMemberCommand(groupId, userId);
            commandBus.Value.Send(Envelope.Create(command));

            var mapper = new UserListItemMapper();
            return mapper.Map(queryService.GetUser(userId));
        }

        public IUserListItem RemoveGroupMember(Guid groupId, Guid userId)
        {
            userContext.CheckPermission(Permissions.UserMaintenance);

            var command = new RemoveGroupMemberCommand(groupId, userId);
            commandBus.Value.Send(Envelope.Create(command));

            var mapper = new UserListItemMapper();
            return mapper.Map(queryService.GetUser(userId));
        }

        public IEnumerable<IGroup> GetUserGroups(Guid userId)
        {
            var mapper = new GroupMapper();
            return mapper.Map(queryService.GetUserGroups(userId));
        }

        public IEnumerable<IRole> GetRoles()
        {
            var mapper = new RoleMapper();
            return mapper.Map(queryService.GetRoles());
        }

        public IRole CreateRole(string name, string description, string externalGroupName)
        {
            userContext.CheckPermission(Permissions.RoleMaintenance);

            Guid id = Guid.NewGuid();
            var command = new CreateRoleCommand(id, name, description, externalGroupName);
            commandBus.Value.Send(Envelope.Create(command));
            return GetRole(id);
        }

        public void UpdateRole(Guid id, string name, string description, string externalGroupName)
        {
            userContext.CheckPermission(Permissions.RoleMaintenance);

            var command = new UpdateRoleCommand(id, name, description, externalGroupName);
            commandBus.Value.Send(Envelope.Create(command));
        }

        public IRole GetRole(Guid roleId)
        {
            var mapper = new RoleMapper();
            return mapper.Map(queryService.GetRole(roleId));
        }

        public void AddUserRole(Guid userId, Guid roleId)
        {
            userContext.CheckPermission(Permissions.UserMaintenance);

            var command = new AddUserRoleCommand(userId, roleId);
            commandBus.Value.Send(Envelope.Create(command));
        }

        public void RemoveUserRole(Guid userId, Guid roleId)
        {
            userContext.CheckPermission(Permissions.UserMaintenance);

            var command = new RemoveUserRoleCommand(userId, roleId);
            commandBus.Value.Send(Envelope.Create(command));
        }

        public IEnumerable<IPermission> GetPermissions()
        {
            var mapper = new PermissionMapper();
            return mapper.Map(queryService.GetPermissions());
        }

        public void AddRolePermission(Guid roleId, Guid permissionId)
        {
            userContext.CheckPermission(Permissions.RoleMaintenance);

            var command = new AddRolePermissionCommand(roleId, permissionId);
            commandBus.Value.Send(Envelope.Create(command));
        }

        public void RemoveRolePermission(Guid roleId, Guid permissionId)
        {
            userContext.CheckPermission(Permissions.RoleMaintenance);

            var command = new RemoveRolePermissionCommand(roleId, permissionId);
            commandBus.Value.Send(Envelope.Create(command));
        }

        public void UpdateGroup(Guid id, string name, string description, bool isDisabled, string externalGroupName)
        {
            userContext.CheckPermission(Permissions.UserMaintenance);

            var command = new UpdateGroupCommand(id, name, description, isDisabled, externalGroupName);
            commandBus.Value.Send(Envelope.Create(command));
        }

        public IEnumerable<IOrganization> GetOrganizations()
        {
            var mapper = new OrganizationMapper();
            return mapper.Map(queryService.GetOrganizations());
        }

        public IOrganization GetOrganization(Guid id)
        {
            var mapper = new OrganizationMapper();
            return mapper.Map(queryService.GetOrganization(id));
        }

        public void UpdateOrganization(Guid id, string name, string description, string email, bool isDisabled)
        {
            userContext.CheckPermission(Permissions.UserMaintenance);

            var command = new UpdateOrganizationCommand(id, name, description, email, isDisabled);
            commandBus.Value.Send(Envelope.Create(command));
        }

        public IOrganization CreateOrganization(string name, string description)
        {
            userContext.CheckPermission(Permissions.UserMaintenance);

            Guid organizationId = Guid.NewGuid();
            ICommand command = new CreateOrganizationCommand(organizationId, name, description);
            commandBus.Value.Send(Envelope.Create(command));
            return GetOrganization(organizationId);
        }
    }
}