using System;
using System.Collections.Generic;

namespace Affecto.IdentityManagement.WebApi.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
        public ICollection<Account> Accounts { get; set; }
        public ICollection<Role> Roles { get; set; }
        public ICollection<Organization> Organizations { get; set; }
        public ICollection<CustomProperty> CustomProperties { get; set; }
    }
}