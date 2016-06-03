using System.Data.Entity;
using Affecto.EntityFramework.PostgreSql.Configuration;
using Affecto.IdentityManagement.Store.EntityFramework;
using Autofac;
using DbContext = Affecto.IdentityManagement.Store.EntityFramework.DbContext;

namespace Affecto.IdentityManagement.Store.PostgreSql
{
    public class ModuleRegistration : EntityFramework.ModuleRegistration
    {
        protected override void RegisterDbContext(ContainerBuilder builder)
        {
            Database.SetInitializer<PostgreDbContext>(null);
            builder.Register(ctx => new PostgreDbContext(PostgreSqlConfiguration.Settings.Schemas[DbContext.ConfigurationKey])).As<IDbContext>();
        }
    }
}