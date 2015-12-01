using Affecto.IdentityManagement.ApplicationServices.Model;
using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.IdentityManagement.ApplicationServices.Mapping
{
    internal class PermissionMapper : OneWayMapper<Querying.Data.Permission, Permission>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<Querying.Data.Permission, Permission>();
        }
    }
}