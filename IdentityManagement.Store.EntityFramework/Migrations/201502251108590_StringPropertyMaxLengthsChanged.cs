using System.Data.Entity.Migrations;

namespace Affecto.IdentityManagement.Store.EntityFramework.Migrations
{
    public partial class StringPropertyMaxLengthsChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Account", "Name", c => c.String(nullable: false, maxLength: 4000));
            AlterColumn("dbo.Group", "Name", c => c.String(nullable: false, maxLength: 4000));
            AlterColumn("dbo.Group", "Description", c => c.String(maxLength: 4000));
            AlterColumn("dbo.User", "Name", c => c.String(nullable: false, maxLength: 4000));
            AlterColumn("dbo.Organization", "Name", c => c.String(nullable: false, maxLength: 4000));
            AlterColumn("dbo.Organization", "Description", c => c.String(maxLength: 4000));
            AlterColumn("dbo.Role", "Name", c => c.String(nullable: false, maxLength: 4000));
            AlterColumn("dbo.Role", "Description", c => c.String(maxLength: 4000));
            AlterColumn("dbo.Permission", "Name", c => c.String(nullable: false, maxLength: 4000));
            AlterColumn("dbo.Permission", "Description", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Permission", "Description", c => c.String(maxLength: 250));
            AlterColumn("dbo.Permission", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Role", "Description", c => c.String(maxLength: 250));
            AlterColumn("dbo.Role", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Organization", "Description", c => c.String(maxLength: 250));
            AlterColumn("dbo.Organization", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.User", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Group", "Description", c => c.String(maxLength: 250));
            AlterColumn("dbo.Group", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Account", "Name", c => c.String(nullable: false, maxLength: 50));
        }
    }
}