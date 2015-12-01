using System;
using System.Linq;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal class AccountExistsQuery
    {
        private readonly IQueryable<Account> accounts;

        public AccountExistsQuery(IQueryable<Account> accounts)
        {
            if (accounts == null)
            {
                throw new ArgumentNullException("accounts");
            }
            this.accounts = accounts;
        }

        public bool Execute(AccountType type, string accountName)
        {
            return accounts.Any(a => a.Type == type && a.Name == accountName);
        }
    }
}