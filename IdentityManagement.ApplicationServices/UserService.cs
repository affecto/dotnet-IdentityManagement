using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.Authentication.Passwords;
using Affecto.IdentityManagement.ApplicationServices.Mapping;
using Affecto.IdentityManagement.Commanding.Commands;
using Affecto.IdentityManagement.Interfaces;
using Affecto.IdentityManagement.Interfaces.Model;
using Affecto.IdentityManagement.Querying;
using Affecto.IdentityManagement.Querying.Data;
using Affecto.IdentityManagement.Querying.Exceptions;
using Affecto.Patterns.Cqrs;
using AccountType = Affecto.IdentityManagement.Interfaces.Model.AccountType;

namespace Affecto.IdentityManagement.ApplicationServices
{
    internal class UserService : IUserService
    {
        private readonly IQueryService queryService;
        private readonly Lazy<ICommandBus> commandBus;

        public UserService(IQueryService queryService, Lazy<ICommandBus> commandBus)
        {
            if (queryService == null)
            {
                throw new ArgumentNullException("queryService");
            }
            if (commandBus == null)
            {
                throw new ArgumentNullException("commandBus");
            }

            this.queryService = queryService;
            this.commandBus = commandBus;
        }

        public bool IsExistingUserAccount(string accountName, AccountType accountType)
        {
            return queryService.IsExistingUser(accountName, (Querying.Data.AccountType)accountType);
        }

        public bool IsMatchingPassword(string accountName, string password)
        {
            string hashedPassword;

            try
            {
                hashedPassword = queryService.GetPassword(accountName);
            }
            catch (EntityNotFoundException)
            {
                return false;
            }

            var providedPassword = new Password(password);

            PasswordMatch match = providedPassword.MatchTo(hashedPassword);
            return (match == PasswordMatch.Success || match == PasswordMatch.SuccessRehashNeeded);
        }

        public IUser GetUser(string accountName, AccountType accountType)
        {
            var mapper = new UserMapper();
            return mapper.Map(queryService.GetUser(accountName, (Querying.Data.AccountType)accountType));
        }

        public Guid AddUser(string accountName, AccountType type, string displayName, IEnumerable<string> authenticatedGroups)
        {
            return AddUser(accountName, type, displayName, authenticatedGroups, null);
        }

        public Guid AddUser(string accountName, AccountType type, string displayName, IEnumerable<string> authenticatedGroups, 
            IEnumerable<KeyValuePair<string, string>> customProperties)
        {
            Guid userId = Guid.NewGuid();
            var properties = customProperties != null ? customProperties.ToList() : null;
            var createUserCommand = new CreateUserCommand(userId, displayName, properties);
            commandBus.Value.Send(Envelope.Create(createUserCommand));

            var createAccountCommand = new CreateExternalUserAccountCommand(userId, type, accountName);
            commandBus.Value.Send(Envelope.Create(createAccountCommand));

            SetRolesAndGroups(userId, authenticatedGroups);

            return userId;
        }

        public void UpdateUserGroupsAndRoles(string accountName, AccountType accountType, IEnumerable<string> authenticatedGroups)
        {
            Guid userId = GetUser(accountName, accountType).Id;

            SetRolesAndGroups(userId, authenticatedGroups);
        }

        private void SetRolesAndGroups(Guid userId, IEnumerable<string> authenticatedGroups)
        {
            List<string> authenticatedGroupList = authenticatedGroups.Where(o => !string.IsNullOrEmpty(o)).ToList();

            SetUserGroups(userId, authenticatedGroupList);
            SetUserRoles(userId, authenticatedGroupList);
        }

        private void SetUserGroups(Guid userId, IReadOnlyList<string> authenticatedGroups)
        {
            IEnumerable<Group> groups = GetGroupsWithExternalGroup(queryService.GetGroups());

            foreach (var group in groups)
            {
                if (authenticatedGroups.Any(o => group.ExternalGroupName.Equals(o, StringComparison.OrdinalIgnoreCase)))
                {
                    AddGroup(userId, group.Id);
                }
                else
                {
                    RemoveGroup(userId, group.Id);
                }
            }
        }

        private void SetUserRoles(Guid userId, IReadOnlyList<string> authenticatedGroups)
        {
            IEnumerable<Role> roles = GetRolesWithExternalGroup(queryService.GetRoles());

            foreach (var role in roles)
            {
                if (authenticatedGroups.Any(o => role.ExternalGroupName.Equals(o, StringComparison.OrdinalIgnoreCase)))
                {
                    AddRole(userId, role.Id);
                }
                else
                {
                    RemoveRole(userId, role.Id);
                }
            }
        }

        private static IEnumerable<Group> GetGroupsWithExternalGroup(IReadOnlyCollection<Group> groups)
        {
            return groups.Where(g => !string.IsNullOrEmpty(g.ExternalGroupName));
        }

        private static IEnumerable<Role> GetRolesWithExternalGroup(IReadOnlyCollection<Role> roles)
        {
            return roles.Where(g => !string.IsNullOrEmpty(g.ExternalGroupName));
        }

        private void AddGroup(Guid userId, Guid groupId)
        {
            var command = new AddGroupMemberCommand(groupId, userId);
            commandBus.Value.Send(Envelope.Create(command));
        }

        private void AddRole(Guid userId, Guid roleId)
        {
            var command = new AddUserRoleCommand(userId, roleId);
            commandBus.Value.Send(Envelope.Create(command));
        }

        private void RemoveGroup(Guid userId, Guid groupId)
        {
            var command = new RemoveGroupMemberCommand(groupId, userId);
            commandBus.Value.Send(Envelope.Create(command));
        }

        private void RemoveRole(Guid userId, Guid roleId)
        {
            var command = new RemoveUserRoleCommand(userId, roleId);
            commandBus.Value.Send(Envelope.Create(command));
        }
    }
}