using Affecto.IdentityManagement.ApplicationServices;
using Affecto.IdentityManagement.Commanding;
using Affecto.IdentityManagement.Commanding.CommandHandlers;
using Affecto.IdentityManagement.Interfaces;
using Affecto.Patterns.Cqrs.Autofac;
using Affecto.Patterns.Domain.Autofac;
using Autofac;

namespace Affecto.IdentityManagement.Autofac
{
    public class ModuleRegistration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<CqrsModule>();
            builder.RegisterModule<DomainModule>();
            builder.RegisterModule<Store.EntityFramework.ModuleRegistration>();

            builder.RegisterType<ApplicationServices.IdentityManagementService>().As<IIdentityManagementService>();
            builder.RegisterType<UserService>().As<IUserService>();

            RegisterCommandHandlers(builder);

            builder.RegisterType<AuditTrailWriter>().As<IAuditTrailWriter>();
        }

        private static void RegisterCommandHandlers(ContainerBuilder builder)
        {
            builder.RegisterType<CreateUserCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<CreateOrganizationCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<UpdateUserCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<CreateUserAccountCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<UpdateUserAccountCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<RemoveUserAccountCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<CreateGroupCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<AddGroupMemberCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<RemoveGroupMemberCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<CreateRoleCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<UpdateOrganizationCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<UpdateRoleCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<UpdateGroupCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<AddUserRoleCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<RemoveUserRoleCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<AddUserOrganizationCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<RemoveUserOrganizationCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<AddRolePermissionCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<RemoveRolePermissionCommandHandler>().AsImplementedInterfaces();
        }
    }
}