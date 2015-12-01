namespace Affecto.IdentityManagement.WebApi.Model
{
    public class UpdateRoleCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExternalGroupName { get; set; }
    }
}