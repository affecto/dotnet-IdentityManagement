namespace Affecto.IdentityManagement.Store.PostgreSql.Migrations
{
    public partial class Initial : IdentityManagementMigration
    {
        public override void Up()
        {
            CreateTable(
                "account",
                c => new
                    {
                        userid = c.Guid(nullable: false),
                        type = c.Int(nullable: false),
                        name = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.userid, t.type })
                .Index(t => t.userid);

            AddForeignKey("account", "userid", "user", "id", true, "fk_user");

            CreateTable(
                "group",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        name = c.String(nullable: false, maxLength: 4000),
                        description = c.String(maxLength: 4000),
                        isdisabled = c.Boolean(nullable: false),
                        externalgroupname = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "user",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        name = c.String(nullable: false, maxLength: 4000),
                        isdisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "organization",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        name = c.String(nullable: false, maxLength: 4000),
                        description = c.String(maxLength: 4000),
                        isdisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "role",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        name = c.String(nullable: false, maxLength: 4000),
                        description = c.String(maxLength: 4000),
                        externalgroupname = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "permission",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        name = c.String(nullable: false, maxLength: 4000),
                        description = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "userorganization",
                c => new
                    {
                        userid = c.Guid(nullable: false),
                        organizationid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.userid, t.organizationid })
                .Index(t => t.userid, name: "IX_UserId")
                .Index(t => t.organizationid, name: "IX_OrganizationId");

            AddForeignKey("userorganization", "userid", "user", "id", true, "fk_user");
            AddForeignKey("userorganization", "organizationid", "organization", "id", true, "fk_organization");

            CreateTable(
                "rolepermission",
                c => new
                    {
                        permissionid = c.Guid(nullable: false),
                        roleid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.permissionid, t.roleid })
                .Index(t => t.permissionid, name: "IX_PermissionId")
                .Index(t => t.roleid, name: "IX_RoleId");

            AddForeignKey("rolepermission", "permissionid", "permission", "id", true, "fk_permission");
            AddForeignKey("rolepermission", "roleid", "role", "id", true, "fk_role");

            CreateTable(
                "userrole",
                c => new
                    {
                        userid = c.Guid(nullable: false),
                        roleid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.userid, t.roleid })
                .Index(t => t.userid, name: "IX_UserId")
                .Index(t => t.roleid, name: "IX_RoleId");

            AddForeignKey("userrole", "userid", "user", "id", true, "fk_user");
            AddForeignKey("userrole", "roleid", "role", "id", true, "fk_role");

            CreateTable(
                "usergroup",
                c => new
                    {
                        groupid = c.Guid(nullable: false),
                        userid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.groupid, t.userid })
                .Index(t => t.groupid, name: "IX_GroupId")
                .Index(t => t.userid, name: "IX_UserId");

            AddForeignKey("usergroup", "groupid", "group", "id", true, "fk_group");
            AddForeignKey("usergroup", "userid", "user", "id", true, "fk_user");
        }
        
        public override void Down()
        {
            DropForeignKey("usergroup", "userid", "user");
            DropForeignKey("usergroup", "groupid", "group");
            DropForeignKey("userrole", "roleid", "role");
            DropForeignKey("userrole", "userid", "user");
            DropForeignKey("rolepermission", "roleid", "role");
            DropForeignKey("rolepermission", "permissionid", "permission");
            DropForeignKey("userorganization", "organizationid", "organization");
            DropForeignKey("userorganization", "userid", "user");
            DropForeignKey("account", "userid", "user");
            DropIndex("usergroup", "IX_UserId");
            DropIndex("usergroup", "IX_GroupId");
            DropIndex("userrole", "IX_RoleId");
            DropIndex("userrole", "IX_UserId");
            DropIndex("rolepermission", "IX_RoleId");
            DropIndex("rolepermission", "IX_PermissionId");
            DropIndex("userorganization", "IX_OrganizationId");
            DropIndex("userorganization", "IX_UserId");
            DropIndex("account", new[] { "userid" });
            DropTable("usergroup");
            DropTable("userrole");
            DropTable("rolepermission");
            DropTable("userorganization");
            DropTable("permission");
            DropTable("role");
            DropTable("organization");
            DropTable("user");
            DropTable("group");
            DropTable("account");
        }
    }
}