namespace Affecto.IdentityManagement.WebApi.Model
{
    public class UpdateUserCommand
    {
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
    }
}