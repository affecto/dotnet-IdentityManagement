namespace Affecto.IdentityManagement.WebApi.Model
{
    public class UpdateOrganizationCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDisabled { get; set; }
    }
}