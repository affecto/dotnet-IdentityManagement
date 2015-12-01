using Affecto.IdentityManagement.Interfaces.Model;
using Affecto.IdentityManagement.WebApi.Model;
using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.IdentityManagement.WebApi.Mapping
{
    internal class OrganizationMapper : OneWayMapper<IOrganization, Organization>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<IOrganization, Organization>();
        }
    }
}