using System;
using System.Collections.Generic;
using System.Web.Http;
using Affecto.IdentityManagement.Interfaces;
using Affecto.IdentityManagement.WebApi.Mapping;
using Affecto.IdentityManagement.WebApi.Model;
using Affecto.Mapping;
using AccountType = Affecto.IdentityManagement.Interfaces.Model.AccountType;

namespace Affecto.IdentityManagement.WebApi
{
    public class IdentityManagementController : ApiController
    {
        private readonly IIdentityManagementService identityManagementService;
        private readonly MapperFactory mapperFactory;

        public IdentityManagementController(IIdentityManagementService identityManagementService)
        {
            if (identityManagementService == null)
            {
                throw new ArgumentNullException("identityManagementService");
            }

            this.identityManagementService = identityManagementService;
            mapperFactory = new MapperFactory();
        }

        [HttpGet]
        [Route("v1/identitymanagement/users")]
        public IHttpActionResult GetUsers()
        {
            ICollection<UserListItem> users = mapperFactory.CreateUserListItemMapper().Map(identityManagementService.GetUsers());
            return Ok(users);
        }

        [HttpGet]
        [Route("v1/identitymanagement/users/{userId}")]
        public IHttpActionResult GetUser(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentException("userId must be defined.");
            }

            User user = mapperFactory.CreateUserMapper().Map(identityManagementService.GetUser(userId));
            return Ok(user);
        }

        [HttpPost]
        [Route("v1/identitymanagement/users")]
        public IHttpActionResult CreateUser(NewUser newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException("newUser");
            }
            if (string.IsNullOrWhiteSpace(newUser.Name))
            {
                throw new ArgumentException("User name must be defined.");
            }

            UserListItem userListItem = mapperFactory.CreateUserListItemMapper().Map(identityManagementService.CreateUser(newUser.Name));

            return Ok(userListItem);
        }

        [HttpPut]
        [Route("v1/identitymanagement/users/{userId}")]
        public IHttpActionResult UpdateUser(Guid userId, UpdateUserCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (string.IsNullOrWhiteSpace(command.Name))
            {
                throw new ArgumentException("User name must be defined.");
            }
            if (userId == Guid.Empty)
            {
                throw new ArgumentException("userId must be defined.");
            }

            identityManagementService.UpdateUser(userId, command.Name, command.IsDisabled);
            return Ok();
        }

        [HttpPost]
        [Route("v1/identitymanagement/users/{userId}/accounts")]
        public IHttpActionResult AddUserAccount(Guid userId, AddUserAccountCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (string.IsNullOrWhiteSpace(command.Name))
            {
                throw new ArgumentException("Account name must be defined.");
            }
            if (userId == Guid.Empty)
            {
                throw new ArgumentException("userId must be defined.");
            }

            AccountType type = ParseAccountType(command.Type);

            if (type == AccountType.Password)
            {
                identityManagementService.AddUserAccount(userId, command.Name, command.Password);
            }
            else
            {
                identityManagementService.AddExternalUserAccount(userId, type, command.Name);
            }

            return Ok();
        }

        [HttpPut]
        [Route("v1/identitymanagement/users/{userId}/accounts")]
        public IHttpActionResult UpdateUserAccount(Guid userId, UpdateUserAccountCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            AccountType type = ParseAccountType(command.Type);
            identityManagementService.UpdateExternalUserAccount(userId, type, command.Name);
            return Ok();
        }

        [HttpDelete]
        [Route("v1/identitymanagement/users/{userId}/accounts")]
        public IHttpActionResult RemoveUserAccount(Guid userId, string accountType, string name)
        {
            if (string.IsNullOrWhiteSpace(accountType))
            {
                throw new ArgumentException("Account type must be defined.");
            }

            AccountType type = ParseAccountType(accountType);
            identityManagementService.RemoveUserAccount(userId, type, name);
            return Ok();
        }

        [HttpPost]
        [Route("v1/identitymanagement/users/{userId}/roles")]
        public IHttpActionResult AddUserRole(Guid userId, AddUserRoleCommand command)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentException("userId must be defined.");
            }
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (command.RoleId == Guid.Empty)
            {
                throw new ArgumentException("User role id must be defined.");
            }

            identityManagementService.AddUserRole(userId, command.RoleId);
            return Ok();
        }

        [HttpDelete]
        [Route("v1/identitymanagement/users/{userId}/roles/{roleId}")]
        public IHttpActionResult RemoveUserRole(Guid userId, Guid roleId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException("userId");
            }
            if (roleId == Guid.Empty)
            {
                throw new ArgumentNullException("roleId");
            }

            identityManagementService.RemoveUserRole(userId, roleId);
            return Ok();
        }

        [HttpPut]
        [Route("v1/identitymanagement/users/{userId}/organizations")]
        public IHttpActionResult AddUserOrganization(Guid userId, [FromBody] Guid organizationId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentException("userId must be defined.");
            }
            if (organizationId == Guid.Empty)
            {
                throw new ArgumentNullException("organizationId");
            }
            identityManagementService.AddUserOrganization(userId, organizationId);
            return Ok();
        }

        [HttpDelete]
        [Route("v1/identitymanagement/users/{userId}/organizations/{organizationId}")]
        public IHttpActionResult RemoveUserOrganization(Guid userId, Guid organizationId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException("userId");
            }
            if (organizationId == Guid.Empty)
            {
                throw new ArgumentNullException("organizationId");
            }
            identityManagementService.RemoveUserOrganization(userId, organizationId);
            return Ok();
        }

        [HttpGet]
        [Route("v1/identitymanagement/groups")]
        public IHttpActionResult GetGroups()
        {
            ICollection<Group> groups = mapperFactory.CreateGroupMapper().Map(identityManagementService.GetGroups());
            return Ok(groups);
        }

        [HttpGet]
        [Route("v1/identitymanagement/groups/{groupId}")]
        public IHttpActionResult GetGroup(Guid groupId)
        {
            if (groupId == Guid.Empty)
            {
                throw new ArgumentException("groupId must be defined.");
            }

            Group group = mapperFactory.CreateGroupMapper().Map(identityManagementService.GetGroup(groupId));
            return Ok(group);
        }

        [HttpPost]
        [Route("v1/identitymanagement/groups")]
        public IHttpActionResult CreateGroup(NewGroup newGroup)
        {
            if (newGroup == null)
            {
                throw new ArgumentNullException("newGroup");
            }
            if (string.IsNullOrWhiteSpace(newGroup.Name))
            {
                throw new ArgumentException("Group name must be defined.");
            }

            Group group = mapperFactory.CreateGroupMapper()
                .Map(identityManagementService.CreateGroup(newGroup.Name, newGroup.Description, newGroup.ExternalGroupName));

            return Ok(group);
        }

        [HttpGet]
        [Route("v1/identitymanagement/groups/{groupId}/members")]
        public IHttpActionResult GetGroupMembers(Guid groupId)
        {
            if (groupId == Guid.Empty)
            {
                throw new ArgumentException("groupId must be defined.");
            }

            ICollection<UserListItem> groupMembers = mapperFactory.CreateUserListItemMapper().Map(identityManagementService.GetGroupMembers(groupId));
            return Ok(groupMembers);
        }

        [HttpPost]
        [Route("v1/identitymanagement/groups/{groupId}/members")]
        public IHttpActionResult AddGroupMember(Guid groupId, NewGroupMember newGroupMember)
        {
            if (groupId == Guid.Empty)
            {
                throw new ArgumentException("groupId must be defined.");
            }
            if (newGroupMember == null)
            {
                throw new ArgumentNullException("newGroupMember");
            }
            if (newGroupMember.UserId == Guid.Empty)
            {
                throw new ArgumentException("User id must be defined.");
            }

            var groupMember = identityManagementService.AddGroupMember(groupId, newGroupMember.UserId);
            UserListItem userListItem = mapperFactory.CreateUserListItemMapper().Map(groupMember);

            return Ok(userListItem);
        }

        [HttpDelete]
        [Route("v1/identitymanagement/groups/{groupId}/members/{memberId}")]
        public IHttpActionResult RemoveGroupMember(Guid groupId, Guid memberId)
        {
            if (groupId == Guid.Empty)
            {
                throw new ArgumentException("groupId must be defined.");
            }
            if (memberId == Guid.Empty)
            {
                throw new ArgumentException("memberId must be defined.");
            }

            var groupMember = identityManagementService.RemoveGroupMember(groupId, memberId);
            UserListItem userListItem = mapperFactory.CreateUserListItemMapper().Map(groupMember);

            return Ok(userListItem);
        }

        [HttpPut]
        [Route("v1/identitymanagement/groups/{groupId}")]
        public IHttpActionResult UpdateGroup(Guid groupId, UpdateGroupCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (groupId == Guid.Empty)
            {
                throw new ArgumentException("groupId must be defined.");
            }
            if (string.IsNullOrWhiteSpace(command.Name))
            {
                throw new ArgumentException("Group name must be defined.");
            }

            identityManagementService.UpdateGroup(groupId, command.Name, command.Description, command.IsDisabled, command.ExternalGroupName);
            return Ok();
        }

        [HttpGet]
        [Route("v1/identitymanagement/roles")]
        public IHttpActionResult GetRoles()
        {
            ICollection<Role> roles = mapperFactory.CreateRoleMapper().Map(identityManagementService.GetRoles());
            return Ok(roles);
        }

        [HttpGet]
        [Route("v1/identitymanagement/roles/{roleId}")]
        public IHttpActionResult GetRole(Guid roleId)
        {
            if (roleId == Guid.Empty)
            {
                throw new ArgumentException("roleId must be defined.");
            }

            Role role = mapperFactory.CreateRoleMapper().Map(identityManagementService.GetRole(roleId));
            return Ok(role);
        }

        [HttpPost]
        [Route("v1/identitymanagement/roles")]
        public IHttpActionResult CreateRole(NewRole newRole)
        {
            if (newRole == null)
            {
                throw new ArgumentNullException("newRole");
            }
            if (string.IsNullOrWhiteSpace(newRole.Name))
            {
                throw new ArgumentException("Role name must be defined.");
            }

            Role role = mapperFactory.CreateRoleMapper().Map(identityManagementService.CreateRole(newRole.Name, newRole.Description, newRole.ExternalGroupName));

            return Ok(role);
        }

        [HttpPut]
        [Route("v1/identitymanagement/roles/{roleId}")]
        public IHttpActionResult UpdateRole(Guid roleId, UpdateRoleCommand command)
        {
            if (roleId == Guid.Empty)
            {
                throw new ArgumentException("roleId must be defined.");
            }
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (string.IsNullOrWhiteSpace(command.Name))
            {
                throw new ArgumentException("Role name must be defined.");
            }

            identityManagementService.UpdateRole(roleId, command.Name, command.Description, command.ExternalGroupName);

            return Ok();
        }

        [HttpPost]
        [Route("v1/identitymanagement/roles/{roleId}/permissions/{permissionId}")]
        public IHttpActionResult AddRolePermission(Guid roleId, Guid permissionId)
        {
            if (roleId == Guid.Empty)
            {
                throw new ArgumentException("roleId must be defined.");
            }
            if (permissionId == Guid.Empty)
            {
                throw new ArgumentException("permissionId must be defined.");
            }

            identityManagementService.AddRolePermission(roleId, permissionId);

            return Ok();
        }

        [HttpDelete]
        [Route("v1/identitymanagement/roles/{roleId}/permissions/{permissionId}")]
        public IHttpActionResult RemoveRolePermission(Guid roleId, Guid permissionId)
        {
            if (roleId == Guid.Empty)
            {
                throw new ArgumentException("roleId must be defined.");
            }
            if (permissionId == Guid.Empty)
            {
                throw new ArgumentException("permissionId must be defined.");
            }

            identityManagementService.RemoveRolePermission(roleId, permissionId);

            return Ok();
        }

        [HttpGet]
        [Route("v1/identitymanagement/permissions")]
        public IHttpActionResult GetPermissions()
        {
            ICollection<Permission> permissions = mapperFactory.CreatePermissionMapper().Map(identityManagementService.GetPermissions());

            return Ok(permissions);
        }

        [HttpGet]
        [Route("v1/identitymanagement/organizations")]
        public IHttpActionResult GetOrganizations()
        {
            ICollection<Organization> organizations = mapperFactory.CreateOrganizationMapper().Map(identityManagementService.GetOrganizations());
            return Ok(organizations);
        }

        [HttpGet]
        [Route("v1/identitymanagement/organizations/{organizationId}")]
        public IHttpActionResult GetOrganization(Guid organizationId)
        {
            if (organizationId == Guid.Empty)
            {
                throw new ArgumentException("organizationId must be ´defined.");
            }

            Organization organization = mapperFactory.CreateOrganizationMapper().Map(identityManagementService.GetOrganization(organizationId));
            return Ok(organization);
        }

        [HttpPut]
        [Route("v1/identitymanagement/organizations/{organizationId}")]
        public IHttpActionResult UpdateOrganization(Guid organizationId, UpdateOrganizationCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (organizationId == Guid.Empty)
            {
                throw new ArgumentException("organizationId must be defined.");
            }
            if (string.IsNullOrWhiteSpace(command.Name))
            {
                throw new ArgumentException("Organization name must be defined.");
            }

            identityManagementService.UpdateOrganization(organizationId, command.Name, command.Description, command.Email, command.IsDisabled);
            return Ok();
        }

        [HttpPost]
        [Route("v1/identitymanagement/organizations")]
        public IHttpActionResult CreateOrganization(NewOrganization organization)
        {
            if (organization == null)
            {
                throw new ArgumentNullException("organization");
            }
            if (string.IsNullOrWhiteSpace(organization.Name))
            {
                throw new ArgumentException("Organization name must be defined.");
            }

            Organization result = mapperFactory.CreateOrganizationMapper().Map(identityManagementService.CreateOrganization(organization.Name, organization.Description, organization.Email));
            return Ok(result);
        }

        private static AccountType ParseAccountType(string accountType)
        {
            return (AccountType)Enum.Parse(typeof(AccountType), accountType);
        }
    }
}