using System.Data.Entity.Migrations;

namespace Affecto.IdentityManagement.Store.EntityFramework.Migrations
{
    public partial class AccountCompositeKeyAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Account", "User_Id", "dbo.User");
            DropIndex("dbo.Account", new[] { "User_Id" });

            // SqlCE doesn't support renaming
            //RenameColumn(table: "dbo.Account", name: "User_Id", newName: "UserId");
            AddColumn("dbo.Account", "UserId", c => c.Guid(nullable: false));
            Sql("UPDATE Account SET UserId = User_Id");
            DropColumn("dbo.Account", "User_Id");

            DropPrimaryKey("dbo.Account");
            AlterColumn("dbo.Account", "UserId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Account", new[] { "UserId", "Type" });
            CreateIndex("dbo.Account", "UserId");
            AddForeignKey("dbo.Account", "UserId", "dbo.User", "Id", cascadeDelete: true);
            DropColumn("dbo.Account", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Account", "Id", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Account", "UserId", "dbo.User");
            DropIndex("dbo.Account", new[] { "UserId" });
            DropPrimaryKey("dbo.Account");
            AlterColumn("dbo.Account", "UserId", c => c.Guid());
            AddPrimaryKey("dbo.Account", "Id");

            //RenameColumn(table: "dbo.Account", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Account", "User_Id", c => c.Guid(nullable: false));
            Sql("UPDATE Account SET User_Id = UserId");
            DropColumn("dbo.Account", "UserId");

            CreateIndex("dbo.Account", "User_Id");
            AddForeignKey("dbo.Account", "User_Id", "dbo.User", "Id");
        }
    }
}