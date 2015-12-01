using Affecto.EntityFramework.PostgreSql;
using Affecto.IdentityManagement.Store.EntityFramework;

namespace Affecto.IdentityManagement.Store.PostgreSql.Migrations
{
    internal sealed class Configuration : HistoryContextDbMigrationsConfiguration<PostgreDbContext>
    {
        public Configuration()
            : base(PostgreSqlConfiguration.Settings.Schemas[DbContext.ConfigurationKey])
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}