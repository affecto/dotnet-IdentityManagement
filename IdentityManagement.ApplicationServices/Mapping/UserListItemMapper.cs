using Affecto.IdentityManagement.ApplicationServices.Model;
using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.IdentityManagement.ApplicationServices.Mapping
{
    internal class UserListItemMapper : OneWayMapper<Querying.Data.User, UserListItem>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<Querying.Data.User, UserListItem>();
        }
    }
}