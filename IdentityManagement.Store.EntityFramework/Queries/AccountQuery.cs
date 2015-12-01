using System;
using System.Linq;
using Affecto.IdentityManagement.Querying.Exceptions;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal class AccountQuery
    {
        private readonly IQueryable<Account> accounts;

        public AccountQuery(IQueryable<Account> accounts)
        {
            if (accounts == null)
            {
                throw new ArgumentNullException("accounts");
            }
            this.accounts = accounts;
        }

        public Account Execute(Guid userId, AccountType type)
        {
            Account account = accounts.SingleOrDefault(a => a.UserId == userId && a.Type == type);
            if (account == null)
            {
                throw new AccountNotFoundException(userId, type);
            }
            return account;
        }
    }
}