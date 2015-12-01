using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal class AccountsQuery
    {
        private readonly IQueryable<Account> accounts;

        public AccountsQuery(IQueryable<Account> accounts)
        {
            if (accounts == null)
            {
                throw new ArgumentNullException("accounts");
            }
            this.accounts = accounts;
        }

        public IEnumerable<Account> Execute(Guid userId)
        {
            return accounts.Where(a => a.UserId == userId);
        }
    }
}