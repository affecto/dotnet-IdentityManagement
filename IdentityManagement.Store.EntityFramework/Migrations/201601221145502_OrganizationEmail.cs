namespace Affecto.IdentityManagement.Store.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganizationEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organization", "Email", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organization", "Email");
        }
    }
}
