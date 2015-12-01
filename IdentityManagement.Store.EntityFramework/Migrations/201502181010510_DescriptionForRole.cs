using System.Data.Entity.Migrations;

namespace Affecto.IdentityManagement.Store.EntityFramework.Migrations
{
    public partial class DescriptionForRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Role", "Description", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Role", "Description");
        }
    }
}