using System.Data.Entity.Migrations;

namespace Affecto.IdentityManagement.Store.EntityFramework.Migrations
{
    public partial class ExternalGroupNameAddedForGroupAndRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Group", "ExternalGroupName", c => c.String(maxLength: 4000));
            AddColumn("dbo.Role", "ExternalGroupName", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Role", "ExternalGroupName");
            DropColumn("dbo.Group", "ExternalGroupName");
        }
    }
}