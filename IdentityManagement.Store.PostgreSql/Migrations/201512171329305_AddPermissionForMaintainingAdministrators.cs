namespace Affecto.IdentityManagement.Store.PostgreSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPermissionForMaintainingAdministrators : RolePermissionMigration
    {
        public override void Up()
        {
            AddPermissionIfNotExists(Identifiers.ManageAdministratorUsersPermissionId.ToString("D"), "MANAGE_ADMINISTRATOR_USERS");
            AddRolePermissionIfNotExists(Identifiers.AdministratorRoleId.ToString("D"), Identifiers.ManageAdministratorUsersPermissionId.ToString("D"));
        }

        public override void Down()
        {
        }
    }
}
