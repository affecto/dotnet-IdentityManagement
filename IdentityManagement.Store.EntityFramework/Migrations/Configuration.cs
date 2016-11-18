using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DbContext>
    {
        protected override void Seed(DbContext context)
        {
            base.Seed(context);

            CreateMissingPermissions(context);

            if (!context.Roles.Any())
            {
                CreateAdminRole(context);
                CreateModifyRole(context);
            }
        }

        private static void CreateMissingPermissions(IDbContext context)
        {
            IEnumerable<Permission> newPermissions = Permissions.Where(o => context.Permissions.All(p => o.Name != p.Name));
            foreach (var permission in newPermissions)
            {
                context.Permissions.AddOrUpdate(o => o.Name, permission);
            }
        }

        private static void CreateAdminRole(IDbContext context)
        {
            var roleAdmin = new Role { Id = Guid.NewGuid(), Name = "P‰‰k‰ytt‰j‰", ExternalGroupName = "Domain Users" };
            foreach (var permission in Permissions)
            {
                roleAdmin.Permissions.Add(context.Permissions.FirstOrDefault(o => o.Name == permission.Name) ?? permission);
            }
            context.Roles.AddOrUpdate(o => o.Name, roleAdmin);
        }

        private static void CreateModifyRole(DbContext context)
        {
            var role = new Role
            {
                Id = Guid.Parse("DA2A9238-EFF2-4EC1-BD4E-3D10844D1613"),
                Name = "Tiedonohjaussuunnitelman muokkaaja"
            };
            role.Permissions.Add(context.Permissions.FirstOrDefault(o => o.Name == "EDIT_CLASSIFICATION"));

            context.Roles.AddOrUpdate(o => o.Name, role);
        }

        private static IEnumerable<Permission> Permissions
        {
            get
            {
                return new[]
                {
                    new Permission { Id = Guid.NewGuid(), Name = "EDIT_CLASSIFICATION", Description = "Tiedonohjaussuunnitelman muutosoikeus" },
                    new Permission { Id = Guid.NewGuid(), Name = "READ_CLASSIFICATION", Description = "Tiedonohjaussuunnitelman lukuoikeus" },
                    new Permission { Id = Guid.NewGuid(), Name = "EDIT_ALL_CLASSES", Description = "Kaikkien rakenneosien muutosoikeus" },
                    new Permission { Id = Guid.NewGuid(), Name = "METADATA_MAINTENANCE", Description = "Metatietom‰‰ritysten muutosoikeus" },
                    new Permission { Id = Guid.NewGuid(), Name = "USER_MAINTENANCE", Description = "K‰ytt‰jien muutosoikeus" },
                    new Permission { Id = Guid.NewGuid(), Name = "ROLE_MAINTENANCE", Description = "Roolien muutosoikeus" },
                    new Permission { Id = Guid.NewGuid(), Name = "VIEW_CLASSIFICATION_VERSIONS", Description = "Kaikkien tiedonohjaussuunnitelman versioiden lukuoikeus" },
                    new Permission { Id = Guid.NewGuid(), Name = "APPROVE_CHANGESETS", Description = "Muutoksien hyv‰ksymisoikeus" },
                    new Permission { Id = Guid.NewGuid(), Name = "IMPORT_CLASSIFICATION", Description = "Tiedonohjaussuunnitelman XML-tuontioikeus" },
                    new Permission { Id = Guid.NewGuid(), Name = "EXPORT_CLASSIFICATION", Description = "Tiedonohjaussuunnitelman XML-vientioikeus" },
                    new Permission { Id = Guid.NewGuid(), Name = "ADD_ROOT_CLASSES", Description = "P‰‰luokan lis‰ysoikeus" },
                    new Permission { Id = Guid.NewGuid(), Name = "VIEW_AUDITTRAIL", Description = "Tapahtumalokin lukuoikeus" }
                };
            }
        }
    }
}