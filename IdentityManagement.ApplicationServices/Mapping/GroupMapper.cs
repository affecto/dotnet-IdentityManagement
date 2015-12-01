using Affecto.IdentityManagement.ApplicationServices.Model;
using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.IdentityManagement.ApplicationServices.Mapping
{
    internal class GroupMapper : OneWayMapper<Querying.Data.Group, Group>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<Querying.Data.Group, Group>();
        }
    }
}