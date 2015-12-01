using Affecto.IdentityManagement.Store.Model;
using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.IdentityManagement.Store.EntityFramework.Mapping
{
    internal class PermissionMapper : OneWayMapper<Permission, Querying.Data.Permission>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<Permission, Querying.Data.Permission>();
        }
    }
}