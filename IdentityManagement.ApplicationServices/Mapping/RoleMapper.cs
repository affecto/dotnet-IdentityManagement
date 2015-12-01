using System.Collections.Generic;
using System.Linq;
using Affecto.IdentityManagement.ApplicationServices.Model;
using Affecto.IdentityManagement.Interfaces.Model;
using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.IdentityManagement.ApplicationServices.Mapping
{
    internal class RoleMapper : OneWayMapper<Querying.Data.Role, Role>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<Querying.Data.Permission, IPermission>().As<Permission>();
            Mapper.CreateMap<Querying.Data.Permission, Permission>();
            Mapper.CreateMap<Querying.Data.Role, Role>()
                .ForMember(s => s.Permissions, m => m.MapFrom(c => Mapper.Map<ICollection<Querying.Data.Permission>, List<IPermission>>(c.Permissions.ToList())));

        }
    }
}