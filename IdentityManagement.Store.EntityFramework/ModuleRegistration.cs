using Affecto.IdentityManagement.Commanding;
using Affecto.IdentityManagement.Querying;
using Autofac;

namespace Affecto.IdentityManagement.Store.EntityFramework
{
    public class ModuleRegistration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            RegisterDbContext(builder);

            builder.RegisterType<QueryService>().As<IQueryService>();
            builder.RegisterType<DbRepository>().As<IDbRepository>();
        }

        protected virtual void RegisterDbContext(ContainerBuilder builder)
        {
            builder.RegisterType<DbContext>().As<IDbContext>();
        }
    }
}