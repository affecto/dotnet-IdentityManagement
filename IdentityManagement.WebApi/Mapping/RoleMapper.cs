using Affecto.IdentityManagement.Interfaces.Model;
using Affecto.IdentityManagement.WebApi.Model;
using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.IdentityManagement.WebApi.Mapping
{
    internal class RoleMapper : OneWayMapper<IRole, Role>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<IPermission, Permission>();
            Mapper.CreateMap<IRole, Role>();
        }
    }
}