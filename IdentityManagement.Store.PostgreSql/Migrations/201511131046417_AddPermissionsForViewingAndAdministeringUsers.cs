namespace Affecto.IdentityManagement.Store.PostgreSql.Migrations
{
    public partial class AddPermissionsForViewingAndAdministeringUsers : IdentityManagementMigration
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

        private void AddPermissionIfNotExists(string permissionId, string permissionName)
        {
            string sql = "INSERT INTO {0} (id, name)"
                + " SELECT '{1}', '{2}'"
                + " WHERE NOT EXISTS (SELECT id FROM {0} WHERE id = '{1}');";

            Sql(string.Format(sql, FormatTableNameWithSchemaNameAndQuotes("permission"), permissionId, permissionName));
        }

        private void AddRolePermissionIfNotExists(string roleId, string permissionId)
        {
            string sql = "INSERT INTO {0} (permissionid, roleid)"
                + " SELECT '{1}', '{2}'"
                + " WHERE NOT EXISTS (SELECT permissionid FROM {0} WHERE permissionid = '{1}' AND roleid = '{2}');";

            Sql(string.Format(sql, FormatTableNameWithSchemaNameAndQuotes("rolepermission"), permissionId, roleId));
        }
    }
}