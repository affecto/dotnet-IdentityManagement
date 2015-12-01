using System.Data.Entity.Migrations;

namespace Affecto.IdentityManagement.Store.EntityFramework.Migrations
{
    public partial class RemovedClassTypeFromAccount : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Account", "ClassType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Account", "ClassType", c => c.Int(nullable: false));
        }
    }
}