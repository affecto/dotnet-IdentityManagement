namespace Affecto.IdentityManagement.WebApi.Model
{
    public class AddUserAccountCommand
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}