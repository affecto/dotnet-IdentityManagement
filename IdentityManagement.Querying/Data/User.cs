using System;
using System.Collections.Generic;

namespace Affecto.IdentityManagement.Querying.Data
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
        public IReadOnlyCollection<Account> Accounts { get; set; }
        public IReadOnlyCollection<Organization> Organizations { get; set; }
        public IReadOnlyCollection<Role> Roles { get; set; }
        public IReadOnlyCollection<Group> Groups { get; set; }
        public IReadOnlyCollection<CustomProperty> CustomProperties { get; set; }

        public User()
        {
            Accounts = new List<Account>();
            Organizations = new List<Organization>();
            Roles = new List<Role>();
            Groups = new List<Group>();
            CustomProperties = new List<CustomProperty>();
        }
    }
}