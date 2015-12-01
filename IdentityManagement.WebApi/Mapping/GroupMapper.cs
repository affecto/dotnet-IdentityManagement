using Affecto.IdentityManagement.Interfaces.Model;
using Affecto.IdentityManagement.WebApi.Model;
using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.IdentityManagement.WebApi.Mapping
{
    internal class GroupMapper : OneWayMapper<IGroup, Group>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<IGroup, Group>();
        }
    }
}