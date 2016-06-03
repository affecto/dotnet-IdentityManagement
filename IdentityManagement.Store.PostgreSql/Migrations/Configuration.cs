using Affecto.EntityFramework.PostgreSql;
using Affecto.EntityFramework.PostgreSql.Configuration;
using Affecto.IdentityManagement.Store.EntityFramework;

namespace Affecto.IdentityManagement.Store.PostgreSql.Migrations
{
    internal sealed class Configuration : PostgreSqlDbMigrationsConfiguration<PostgreDbContext>
    {
        public Configuration()
            : base(PostgreSqlConfiguration.Settings.Schemas[DbContext.ConfigurationKey])
        {
        }
    }
}