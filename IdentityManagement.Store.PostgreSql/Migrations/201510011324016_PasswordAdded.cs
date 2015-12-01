namespace Affecto.IdentityManagement.Store.PostgreSql.Migrations
{
    public partial class PasswordAdded : IdentityManagementMigration
    {
        public override void Up()
        {
            AddColumn("account", "password", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("account", "password");
        }
    }
}