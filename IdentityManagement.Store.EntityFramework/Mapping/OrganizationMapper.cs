using Affecto.IdentityManagement.Store.Model;
using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.IdentityManagement.Store.EntityFramework.Mapping
{
    internal class OrganizationMapper : OneWayMapper<Organization, Querying.Data.Organization>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<Organization, Querying.Data.Organization>();
        }
    }
}