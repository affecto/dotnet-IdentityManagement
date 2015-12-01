using System;

namespace Affecto.IdentityManagement.Store.PostgreSql.Migrations
{
    public partial class CreateAdminUser : IdentityManagementMigration
    {
        private static readonly Guid AdminUserId = Guid.Parse("26B18DF8-8D44-4819-9711-0AA6A32D828D");

        private static readonly Guid OrganizationId = Guid.Parse("7B45E3BC-EDA9-4F6B-97BB-E9354DB660B5");

        public override void Up()
        {
            AddRoleIfNotExists(Identifiers.AdministratorRoleId.ToString("D"), "PTV-p‰‰k‰ytt‰j‰");
            AddRoleIfNotExists(Identifiers.OrganizationAdministratorRoleId.ToString("D"), "Organisaation p‰‰k‰ytt‰j‰");
            AddRoleIfNotExists(Identifiers.BasicUserRoleId.ToString("D"), "Yll‰pit‰j‰");

            AddUser(AdminUserId.ToString("D"), Identifiers.AdministratorRoleId.ToString("D"), "P‰‰k‰ytt‰j‰", "PTV", "admin@ptv.fi",
                "1F400.ANGMoLseyPbyC33X19JdLEoU4z2rSFPLP3z4OT5AA0UsQg4dIRIOokp37ZTWVmn5UA==", OrganizationId.ToString("D"));
        }
        
        public override void Down()
        {
        }

        private void AddRoleIfNotExists(string id, string name)
        {
            string sql = "INSERT INTO {0} (id, name, description, externalgroupname)"
                + " SELECT '{1}', '{2}', NULL, NULL"
                + " WHERE NOT EXISTS (SELECT id FROM {0} WHERE id = '{1}');";

            Sql(string.Format(sql, FormatTableNameWithSchemaNameAndQuotes("role"), id, name));
        }

        private void AddUser(string userId, string roleId, string lastName, string firstName, string email, string password, string organizationId)
        {
            // Custom properties:
            Guid organizationPropertyId = AddCustomProperty("OrganizationId", organizationId);
            Guid emailPropertyId = AddCustomProperty("EmailAddress", email);
            Guid lastNamePropertyId = AddCustomProperty("LastName", lastName);
            Guid firstNamePropertyId = AddCustomProperty("FirstName", firstName);

            // User:
            string sql = "INSERT INTO {0} (id, name, isdisabled)" 
                + " SELECT '{1}', '{2}', FALSE"
                + " WHERE NOT EXISTS (SELECT id FROM {0});";
            Sql(string.Format(sql, FormatTableNameWithSchemaNameAndQuotes("user"), userId, lastName + " " + firstName));

            AddCustomPropertyLink(userId, organizationPropertyId.ToString("D"));
            AddCustomPropertyLink(userId, emailPropertyId.ToString("D"));
            AddCustomPropertyLink(userId, lastNamePropertyId.ToString("D"));
            AddCustomPropertyLink(userId, firstNamePropertyId.ToString("D"));

            // Account:
            sql = "INSERT INTO {0} (userid, type, name, password)"
                + " SELECT '{1}', 2, '{2}', '{3}'"
                + " WHERE NOT EXISTS (SELECT userid FROM {0});";
            Sql(string.Format(sql, FormatTableNameWithSchemaNameAndQuotes("account"), userId, email, password));

            // User role:
            sql = "INSERT INTO {0} (userid, roleid)"
                + " SELECT '{1}', '{2}'"
                + " WHERE NOT EXISTS (SELECT userid FROM {0});";
            Sql(string.Format(sql, FormatTableNameWithSchemaNameAndQuotes("userrole"), userId, roleId));
        }

        private Guid AddCustomProperty(string name, string value)
        {
            Guid customPropertyId = Guid.NewGuid();

            string sql = "INSERT INTO {0} (id, name, value)"
                + " SELECT '{2}', '{3}', '{4}'"
                + " WHERE NOT EXISTS (SELECT id FROM {1});";
            Sql(string.Format(sql, FormatTableNameWithSchemaNameAndQuotes("customproperty"), FormatTableNameWithSchemaNameAndQuotes("user"), customPropertyId,
                name, value));

            return customPropertyId;
        }

        private void AddCustomPropertyLink(string userId, string customPropertyId)
        {
            string sql = "INSERT INTO {0} (userid, custompropertyid)"
                + " SELECT '{1}', '{2}'"
                + " WHERE NOT EXISTS (SELECT userid FROM {0} WHERE userid = '{1}' AND custompropertyid = '{2}');";
            Sql(string.Format(sql, FormatTableNameWithSchemaNameAndQuotes("usercustomproperty"), userId, customPropertyId));
        }
    }
}