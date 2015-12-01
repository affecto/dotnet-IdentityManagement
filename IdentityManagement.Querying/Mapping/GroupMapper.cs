using Affecto.IdentityManagement.Store.Model;
using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.IdentityManagement.Querying.Mapping
{
    internal class GroupMapper : OneWayMapper<Group, Data.Group>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<Group, Data.Group>();
        }
    }
}