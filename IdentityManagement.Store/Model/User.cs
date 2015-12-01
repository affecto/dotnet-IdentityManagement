using System;
using System.Collections.Generic;
using System.Linq;

namespace Affecto.IdentityManagement.Store.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }

        public virtual List<Account> Accounts { get; set; }
        public virtual List<Role> Roles { get; set; }
        public virtual List<Group> Groups { get; set; }
        public virtual List<Organization> Organizations { get; set; }
        public virtual List<CustomProperty> CustomProperties { get; set; }

        public User()
        {
            Accounts = new List<Account>();
            Roles = new List<Role>();
            Groups = new List<Group>();
            Organizations = new List<Organization>();
            CustomProperties = new List<CustomProperty>();
        }

        public bool HasRole(Guid roleId)
        {
            return Roles.Any(r => r.Id.Equals(roleId));
        }

        public void AddRole(Role role)
        {
            Roles.Add(role);
        }

        public Role GetRole(Guid roleId)
        {
            return Roles.First(r => r.Id.Equals(roleId));
        }

        public void RemoveRole(Guid roleId)
        {
            Roles.RemoveAll(r => r.Id.Equals(roleId));
        }

        public bool IsInOrganization(Guid organizationId)
        {
            return Organizations.Any(o => o.Id == organizationId);
        }

        public void IncludeInOrganization(Organization organization)
        {
            Organizations.Add(organization);
        }

        public void RemoveFromOrganization(Organization organization)
        {
            Organizations.Remove(organization);
        }

        public bool HasAccount(AccountType type, string name)
        {
            return Accounts.Any(MatchUserAccount(type, name));
        }

        public Account GetAccount(AccountType type, string name)
        {
            return Accounts.Single(MatchUserAccount(type, name)) ;
        }

        public bool HasAccountType(AccountType type)
        {
            return Accounts.Any(o => o.Type == type);
        }

        public void RemoveUserAccount(AccountType type, string name)
        {
            Accounts.RemoveAll(MatchUserAccount(type, name).Invoke);
        }

        private static Func<Account, bool> MatchUserAccount(AccountType type, string name)
        {
            return account => account.Type == type && account.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}