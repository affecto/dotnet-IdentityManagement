
using Affecto.IdentityManagement.Interfaces.Model;

namespace Affecto.IdentityManagement.ApplicationServices.Model
{
    internal class Account : IAccount
    {
        public AccountType Type { get; set; }
        public string Name { get; set; }
    }
}