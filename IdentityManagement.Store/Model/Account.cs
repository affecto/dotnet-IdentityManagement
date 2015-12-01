using System;

namespace Affecto.IdentityManagement.Store.Model
{
    public class Account
    {
        public AccountType Type { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Guid UserId { get; set; }
    }
}