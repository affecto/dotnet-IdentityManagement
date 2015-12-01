using System.Data.Entity.Migrations;

namespace Affecto.IdentityManagement.Store.EntityFramework.Migrations
{
    public partial class UserCustomPropertiesAndPassword : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomProperty",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        Value = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserCustomProperty",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        CustomPropertyId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CustomPropertyId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.CustomProperty", t => t.CustomPropertyId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CustomPropertyId);
            
            AddColumn("dbo.Account", "Password", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCustomProperty", "CustomPropertyId", "dbo.CustomProperty");
            DropForeignKey("dbo.UserCustomProperty", "UserId", "dbo.User");
            DropIndex("dbo.UserCustomProperty", new[] { "CustomPropertyId" });
            DropIndex("dbo.UserCustomProperty", new[] { "UserId" });
            DropColumn("dbo.Account", "Password");
            DropTable("dbo.UserCustomProperty");
            DropTable("dbo.CustomProperty");
        }
    }
}