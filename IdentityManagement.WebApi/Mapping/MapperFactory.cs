using Affecto.IdentityManagement.Interfaces.Model;
using Affecto.IdentityManagement.WebApi.Model;
using Affecto.Mapping;

namespace Affecto.IdentityManagement.WebApi.Mapping
{
    public class MapperFactory
    {
        public virtual IMapper<IUser, User> CreateUserMapper()
        {
            return new UserMapper();
        }

        public virtual IMapper<IUserListItem, UserListItem> CreateUserListItemMapper()
        {
            return new UserListItemMapper();
        }

        public virtual IMapper<IGroup, Group> CreateGroupMapper()
        {
            return new GroupMapper();
        }

        public virtual IMapper<IRole, Role> CreateRoleMapper()
        {
            return new RoleMapper();
        }

        public virtual IMapper<IPermission, Permission> CreatePermissionMapper()
        {
            return new PermissionMapper();
        }

        public virtual IMapper<IOrganization, Organization> CreateOrganizationMapper()
        {
            return new OrganizationMapper();
        }
    }
}