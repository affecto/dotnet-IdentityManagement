using Affecto.IdentityManagement.Store.Model;
using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.IdentityManagement.Store.EntityFramework.Mapping
{
    internal class UserMapper : OneWayMapper<User, Querying.Data.User>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<Account, Querying.Data.Account>();
            Mapper.CreateMap<Permission, Querying.Data.Permission>();
            Mapper.CreateMap<Group, Querying.Data.Group>();
            Mapper.CreateMap<Organization, Querying.Data.Organization>();
            Mapper.CreateMap<Role, Querying.Data.Role>();
            Mapper.CreateMap<CustomProperty, Querying.Data.CustomProperty>();
            Mapper.CreateMap<User, Querying.Data.User>();
        }
    }
}