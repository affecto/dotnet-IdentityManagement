using System;
using System.Linq;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal class UserByAccountQuery
    {
        private readonly UserQueryBuilder queryBuilder;

        public UserByAccountQuery(IQueryable<User> users)
        {
            queryBuilder = new UserQueryBuilder(users);
        }

        public User Execute(string accountName, AccountType accountType)
        {
            return queryBuilder.IncludeAll()
                .SingleOrDefault(u => u.Accounts.Any(a => a.Name.Equals(accountName, StringComparison.OrdinalIgnoreCase) && a.Type == accountType));
        }
    }
}