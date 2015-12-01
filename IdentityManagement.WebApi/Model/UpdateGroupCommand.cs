namespace Affecto.IdentityManagement.WebApi.Model
{
    public class UpdateGroupCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDisabled { get; set; }
        public string ExternalGroupName { get; set; }
    }
}