using Affecto.IdentityManagement.Interfaces.Model;

namespace Affecto.IdentityManagement.ApplicationServices.Model
{
    internal class CustomProperty : ICustomProperty
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}