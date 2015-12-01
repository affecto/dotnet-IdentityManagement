using System;
using System.Linq;
using System.Linq.Expressions;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal class UserByAccountQuery : UserQuery<UserByAccountQuery.UserAccount>
    {
        public UserByAccountQuery(IQueryable<User> users)
            : base(users)
        {
        }

        protected override Expression<Func<User, bool>> GetUserIdentifierMatchesPredicate(UserAccount userId)
        {
            return user => user.Accounts.Any(account => 
                account.Name.Equals(userId.AccountName, StringComparison.OrdinalIgnoreCase) && account.Type == userId.AccountType);
        }

        internal class UserAccount
        {
            public string AccountName { get; private set; }
            public AccountType AccountType { get; private set; }

            public UserAccount(string accountName, AccountType accountType)
            {
                AccountName = accountName;
                AccountType = accountType;
            }
        }
    }
}