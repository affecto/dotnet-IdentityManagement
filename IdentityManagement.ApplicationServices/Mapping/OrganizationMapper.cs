using Affecto.IdentityManagement.ApplicationServices.Model;
using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.IdentityManagement.ApplicationServices.Mapping
{
    internal class OrganizationMapper : OneWayMapper<Querying.Data.Organization, Organization>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<Querying.Data.Organization, Organization>();
        }
    }
}