using Affecto.IdentityManagement.Store.Model;
using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.IdentityManagement.Store.EntityFramework.Mapping
{
    internal class AccountMapper : OneWayMapper<Account, Querying.Data.Account>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<Account, Querying.Data.Account>();
        }
    }
}