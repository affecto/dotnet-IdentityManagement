using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework
{
    internal class DbContext : System.Data.Entity.DbContext, IDbContext
    {
        public const string ConfigurationKey = "IdentityManagementDbContext";

        public DbContext()
            : base(ConfigurationKey)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        protected DbContext(DbConnection connection)
            : base(connection, true)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public IDbSet<Account> Accounts { get; set; }
        public IDbSet<Group> Groups { get; set; }
        public IDbSet<Permission> Permissions { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Organization> Organizations { get; set; }
        public IDbSet<CustomProperty> CustomProperties { get; set; }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            SpecifyAccount(modelBuilder);
            SpecifyGroup(modelBuilder);
            SpecifyPermission(modelBuilder);
            SpecifyUser(modelBuilder);
            SpecifyRole(modelBuilder);
            SpecifyOrganization(modelBuilder);
            SpecifyCustomProperty(modelBuilder);
        }

        protected virtual string FormatTableName(string tableName)
        {
            return tableName;
        }

        private void SpecifyOrganization(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>().HasKey(o => o.Id);
            modelBuilder.Entity<Organization>().Property(o => o.Name).HasMaxLength(4000).IsRequired();
            modelBuilder.Entity<Organization>().Property(o => o.Description).HasMaxLength(4000);
        }

        private void SpecifyAccount(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasKey(c => new { c.UserId, c.Type });
            modelBuilder.Entity<Account>().Property(a => a.Name).HasMaxLength(4000).IsRequired();
            modelBuilder.Entity<Account>().Property(a => a.Password).HasMaxLength(4000);
        }

        private void SpecifyRole(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasKey(r => r.Id);
            modelBuilder.Entity<Role>().Property(o => o.Name).HasMaxLength(4000).IsRequired();
            modelBuilder.Entity<Role>().Property(o => o.Description).HasMaxLength(4000);
            modelBuilder.Entity<Role>().Property(o => o.ExternalGroupName).HasMaxLength(4000);
            modelBuilder.Entity<Role>().HasMany(r => r.Permissions);
        }

        private void SpecifyGroup(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().HasKey(g => g.Id);
            modelBuilder.Entity<Group>().Property(g => g.Name).HasMaxLength(4000).IsRequired();
            modelBuilder.Entity<Group>().Property(g => g.Description).HasMaxLength(4000);
            modelBuilder.Entity<Group>().Property(g => g.ExternalGroupName).HasMaxLength(4000);
            modelBuilder.Entity<Group>().HasMany(g => g.Users).WithMany(u => u.Groups).Map(m =>
            {
                m.MapLeftKey("GroupId");
                m.MapRightKey("UserId");
                m.ToTable(FormatTableName("UserGroup"));
            });
        }

        private void SpecifyPermission(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().HasKey(p => p.Id);
            modelBuilder.Entity<Permission>().Property(p => p.Name).HasMaxLength(4000).IsRequired();
            modelBuilder.Entity<Permission>().Property(p => p.Description).HasMaxLength(4000);
            modelBuilder.Entity<Permission>().HasMany(p => p.Roles).WithMany(r => r.Permissions).Map(m =>
            {
                m.MapLeftKey("PermissionId");
                m.MapRightKey("RoleId");
                m.ToTable(FormatTableName("RolePermission"));
            });
        }

        private void SpecifyCustomProperty(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomProperty>().HasKey(p => p.Id);
            modelBuilder.Entity<CustomProperty>().Property(p => p.Name).HasMaxLength(1000).IsRequired();
            modelBuilder.Entity<CustomProperty>().Property(p => p.Value).HasMaxLength(4000);
        }

        private void SpecifyUser(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.Name).HasMaxLength(4000).IsRequired();
            modelBuilder.Entity<User>().HasMany(u => u.Accounts);
            modelBuilder.Entity<User>().HasMany(u => u.Groups);

            modelBuilder.Entity<User>().HasMany(u => u.Organizations).WithMany().Map(m =>
            {
                m.MapLeftKey("UserId");
                m.MapRightKey("OrganizationId");
                m.ToTable(FormatTableName("UserOrganization"));
            });

            modelBuilder.Entity<User>().HasMany(u => u.Roles).WithMany().Map(m =>
            {
                m.MapLeftKey("UserId");
                m.MapRightKey("RoleId");
                m.ToTable(FormatTableName("UserRole"));
            });

            modelBuilder.Entity<User>().HasMany(u => u.CustomProperties).WithMany().Map(m =>
            {
                m.MapLeftKey("UserId");
                m.MapRightKey("CustomPropertyId");
                m.ToTable(FormatTableName("UserCustomProperty"));
            });
        }
    }
}