using System.Web.Http.Controllers;
using Affecto.IdentityManagement.WebApi.RequestHandling;
using Affecto.WebApi.Toolkit;
using Autofac;
using Autofac.Integration.WebApi;
using Module = Autofac.Module;

namespace Affecto.IdentityManagement.WebApi
{
    public class ModuleRegistration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<Autofac.ModuleRegistration>();

            builder.RegisterType<IdentityManagementController>().InstancePerRequest();
            RegisterControllerFilters<IdentityManagementController>(builder);
        }

        private static void RegisterControllerFilters<TController>(ContainerBuilder builder) where TController : IHttpController
        {
            builder.RegisterType<WebApiRequestExceptionFilter>()
                .AsWebApiExceptionFilterFor<TController>()
                .InstancePerRequest();

            builder.RegisterType<RequestLoggingFilter>()
               .AsWebApiActionFilterFor<TController>()
               .InstancePerRequest();

            builder.RegisterType<AuthorizationLoggingFilter>()
                .AsWebApiAuthorizationFilterFor<TController>()
                .InstancePerRequest();

            builder.RegisterType<RequireHttpsAttribute>()
                .AsWebApiAuthorizationFilterFor<TController>()
                .InstancePerRequest();
        }
    }
}