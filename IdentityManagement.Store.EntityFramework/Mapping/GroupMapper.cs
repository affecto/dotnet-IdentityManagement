using Affecto.IdentityManagement.Store.Model;
using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.IdentityManagement.Store.EntityFramework.Mapping
{
    internal class GroupMapper : OneWayMapper<Group, Querying.Data.Group>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<Group, Querying.Data.Group>();
        }
    }
}