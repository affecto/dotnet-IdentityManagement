using Affecto.IdentityManagement.Store.Model;
using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.IdentityManagement.Querying.Mapping
{
    internal class PermissionMapper : OneWayMapper<Permission, Data.Permission>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<Permission, Data.Permission>();
        }
    }
}