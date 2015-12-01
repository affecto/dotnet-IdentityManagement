using Affecto.IdentityManagement.Store.Model;
using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.IdentityManagement.Querying.Mapping
{
    internal class RolePartialMapper : OneWayMapper<Role, Data.Role>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<Role, Data.Role>()
                .ForMember(dest => dest.Permissions, opt => opt.Ignore());
            ;
        }
    }
}