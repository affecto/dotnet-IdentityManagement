namespace Affecto.IdentityManagement.Store.PostgreSql.Migrations
{
    public partial class UserCustomPropertiesAdded : IdentityManagementMigration
    {
        public override void Up()
        {
            CreateTable(
                "customproperty",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        name = c.String(nullable: false, maxLength: 1000),
                        value = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "usercustomproperty",
                c => new
                    {
                        userid = c.Guid(nullable: false),
                        custompropertyid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.userid, t.custompropertyid })
                .Index(t => t.userid, name: "IX_UserId")
                .Index(t => t.custompropertyid, name: "IX_CustomPropertyId");

            AddForeignKey("usercustomproperty", "userid", "user", "id", true, "fk_user");
            AddForeignKey("usercustomproperty", "custompropertyid", "customproperty", "id", true, "fk_customproperty");

        }
        
        public override void Down()
        {
            DropForeignKey("usercustomproperty", "custompropertyid", "customproperty");
            DropForeignKey("usercustomproperty", "userid", "user");
            DropIndex("usercustomproperty", "IX_CustomPropertyId");
            DropIndex("usercustomproperty", "IX_UserId");
            DropTable("usercustomproperty");
            DropTable("customproperty");
        }
    }
}