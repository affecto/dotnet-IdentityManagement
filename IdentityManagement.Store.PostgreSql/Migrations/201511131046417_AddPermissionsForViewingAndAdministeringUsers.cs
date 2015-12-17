namespace Affecto.IdentityManagement.Store.PostgreSql.Migrations
{
    public partial class AddPermissionsForViewingAndAdministeringUsers : RolePermissionMigration
    {
        public override void Up()
        {
            AddPermissionIfNotExists(Identifiers.UserMaintenancePermissionId.ToString("D"), "USER_MAINTENANCE");
            AddPermissionIfNotExists(Identifiers.ViewAllUsersPermissionId.ToString("D"), "VIEW_ALL_USERS");
            AddPermissionIfNotExists(Identifiers.ViewUserOrganizationUsersPermissionId.ToString("D"), "VIEW_USER_ORGANIZATION_USERS");

            AddRolePermissionIfNotExists(Identifiers.AdministratorRoleId.ToString("D"), Identifiers.UserMaintenancePermissionId.ToString("D"));
            AddRolePermissionIfNotExists(Identifiers.AdministratorRoleId.ToString("D"), Identifiers.ViewAllUsersPermissionId.ToString("D"));
            AddRolePermissionIfNotExists(Identifiers.AdministratorRoleId.ToString("D"), Identifiers.ViewUserOrganizationUsersPermissionId.ToString("D"));

            AddRolePermissionIfNotExists(Identifiers.OrganizationAdministratorRoleId.ToString("D"), Identifiers.UserMaintenancePermissionId.ToString("D"));
            AddRolePermissionIfNotExists(Identifiers.OrganizationAdministratorRoleId.ToString("D"), Identifiers.ViewUserOrganizationUsersPermissionId.ToString("D"));

            AddRolePermissionIfNotExists(Identifiers.BasicUserRoleId.ToString("D"), Identifiers.ViewUserOrganizationUsersPermissionId.ToString("D"));
        }
        
        public override void Down()
        {
        }
    }
}