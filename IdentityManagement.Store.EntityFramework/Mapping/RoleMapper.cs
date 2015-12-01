using Affecto.IdentityManagement.Store.Model;
using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.IdentityManagement.Store.EntityFramework.Mapping
{
    internal class RoleMapper : OneWayMapper<Role, Querying.Data.Role>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<Role, Querying.Data.Role>();
            Mapper.CreateMap<Permission, Querying.Data.Permission>();
        }
    }
}