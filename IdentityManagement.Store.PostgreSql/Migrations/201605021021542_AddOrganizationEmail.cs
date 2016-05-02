namespace Affecto.IdentityManagement.Store.PostgreSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrganizationEmail : IdentityManagementMigration
    {
        public override void Up()
        {
            AddColumn("organization", "email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("organization", "email");
        }
    }
}
