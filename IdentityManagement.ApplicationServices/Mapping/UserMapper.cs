using System.Collections.Generic;
using Affecto.IdentityManagement.ApplicationServices.Model;
using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.IdentityManagement.ApplicationServices.Mapping
{
    internal class UserMapper : OneWayMapper<Querying.Data.User, User>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<Querying.Data.Account, Account>();
            Mapper.CreateMap<Querying.Data.Group, Group>();
            Mapper.CreateMap<Querying.Data.Organization, Organization>()
                .ForMember(dest => dest.Description, opt => opt.Ignore());
            Mapper.CreateMap<Querying.Data.Role, Role>()
                .ForMember(dest => dest.Permissions, m => m.ResolveUsing(src => Mapper.Map<IReadOnlyCollection<Querying.Data.Permission>, List<Permission>>(src.Permissions)));
            Mapper.CreateMap<Querying.Data.Permission, Permission>();
            Mapper.CreateMap<Querying.Data.CustomProperty, CustomProperty>();
            Mapper.CreateMap<Querying.Data.User, User>()
                .ForMember(dest => dest.Organizations, opt => opt.Ignore())
                .ForMember(dest => dest.Roles, m => m.ResolveUsing(src => Mapper.Map<IReadOnlyCollection<Querying.Data.Role>, List<Role>>(src.Roles)))
                .ForMember(dest => dest.Groups, m => m.ResolveUsing(src => Mapper.Map<IReadOnlyCollection<Querying.Data.Group>, List<Group>>(src.Groups)))
                .ForMember(dest => dest.Organizations, m => m.ResolveUsing(src => Mapper.Map<IReadOnlyCollection<Querying.Data.Organization>, List<Organization>>(src.Organizations)))
                .ForMember(dest => dest.Accounts, m => m.ResolveUsing(src => Mapper.Map<IReadOnlyCollection<Querying.Data.Account>, List<Account>>(src.Accounts)))
                .ForMember(dest => dest.CustomProperties, m => m.ResolveUsing(src => Mapper.Map<IReadOnlyCollection<Querying.Data.CustomProperty>, List<CustomProperty>>(src.CustomProperties)));
        }
    }
}