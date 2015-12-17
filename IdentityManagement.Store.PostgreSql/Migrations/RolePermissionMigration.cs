namespace Affecto.IdentityManagement.Store.PostgreSql.Migrations
{
    public abstract class RolePermissionMigration : IdentityManagementMigration
    {
        protected void AddPermissionIfNotExists(string permissionId, string permissionName)
        {
            string sql = "INSERT INTO {0} (id, name)"
                + " SELECT '{1}', '{2}'"
                + " WHERE NOT EXISTS (SELECT id FROM {0} WHERE id = '{1}');";

            Sql(string.Format(sql, FormatTableNameWithSchemaNameAndQuotes("permission"), permissionId, permissionName));
        }

        protected void AddRolePermissionIfNotExists(string roleId, string permissionId)
        {
            string sql = "INSERT INTO {0} (permissionid, roleid)"
                + " SELECT '{1}', '{2}'"
                + " WHERE NOT EXISTS (SELECT permissionid FROM {0} WHERE permissionid = '{1}' AND roleid = '{2}');";

            Sql(string.Format(sql, FormatTableNameWithSchemaNameAndQuotes("rolepermission"), permissionId, roleId));
        }
    }
}
