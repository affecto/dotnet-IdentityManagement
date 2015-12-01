using Affecto.IdentityManagement.Interfaces.Model;
using Affecto.IdentityManagement.WebApi.Model;
using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.IdentityManagement.WebApi.Mapping
{
    internal class UserListItemMapper : OneWayMapper<IUserListItem, UserListItem>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<IUserListItem, UserListItem>();
        }
    }
}