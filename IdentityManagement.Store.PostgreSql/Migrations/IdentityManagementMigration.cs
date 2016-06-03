using Affecto.EntityFramework.PostgreSql;
using Affecto.EntityFramework.PostgreSql.Configuration;
using Affecto.IdentityManagement.Store.EntityFramework;

namespace Affecto.IdentityManagement.Store.PostgreSql.Migrations
{
    public abstract class IdentityManagementMigration : PostgreSqlDbMigration
    {
        protected override string ResolveSchemaName()
        {
            return PostgreSqlConfiguration.Settings.Schemas[DbContext.ConfigurationKey];
        }
    }
}