using System;
using System.Data.Common;
using System.Data.Entity;
using Affecto.EntityFramework.PostgreSql;
using DbContext = Affecto.IdentityManagement.Store.EntityFramework.DbContext;

namespace Affecto.IdentityManagement.Store.PostgreSql
{
    internal class PostgreDbContext : DbContext
    {
        private readonly string schemaName;

        public PostgreDbContext()
            : this("")
        {
        }

        public PostgreDbContext(string schemaName)
        {
            if (schemaName == null)
            {
                throw new ArgumentNullException("schemaName");
            }

            this.schemaName = schemaName;
        }

        protected PostgreDbContext(DbConnection connection)
            : base(connection)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(schemaName);
            modelBuilder.Conventions.Add(new LowerCasePropertyNameConvention());

            modelBuilder.Types().Configure(configuration =>
            {
                string name = configuration.ClrType.Name.ToLower();
                configuration.ToTable(name);
            });

            modelBuilder.Properties().Configure(configuration =>
            {
                string name = configuration.ClrPropertyInfo.Name.ToLower();
                configuration.HasColumnName(name);
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override string FormatTableName(string tableName)
        {
            return tableName.ToLower();
        }
    }
}