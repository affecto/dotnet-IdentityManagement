using Affecto.IdentityManagement.Store.Model;
using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.IdentityManagement.Querying.Mapping
{
    internal class UserMapper : OneWayMapper<User, Data.User>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<Account, Data.Account>();
            Mapper.CreateMap<CustomProperty, Data.CustomProperty>();
            Mapper.CreateMap<User, Data.User>();
        }
    }
}