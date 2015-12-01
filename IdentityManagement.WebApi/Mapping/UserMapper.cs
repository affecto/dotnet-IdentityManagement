using Affecto.IdentityManagement.Interfaces.Model;
using Affecto.IdentityManagement.WebApi.Model;
using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.IdentityManagement.WebApi.Mapping
{
    internal class UserMapper : OneWayMapper<IUser, User>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<IAccount, Account>();
            Mapper.CreateMap<IOrganization, Organization>();
            Mapper.CreateMap<IPermission, Permission>();
            Mapper.CreateMap<IRole, Role>();
            Mapper.CreateMap<ICustomProperty, CustomProperty>();
            Mapper.CreateMap<IUser, User>();
        }
    }
}