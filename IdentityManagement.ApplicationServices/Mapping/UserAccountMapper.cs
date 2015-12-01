using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.IdentityManagement.ApplicationServices.Mapping
{
    internal class UserAccountMapper : OneWayMapper<Querying.Data.Account, Model.Account>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<Querying.Data.Account, Model.Account>();
        }
    }
}