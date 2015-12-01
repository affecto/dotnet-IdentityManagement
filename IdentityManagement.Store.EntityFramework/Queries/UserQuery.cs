using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal abstract class UserQuery<TIdentifier>
    {
        private readonly IQueryable<User> users;

        protected UserQuery(IQueryable<User> users)
        {
            if (users == null)
            {
                throw new ArgumentNullException("users");
            }
            this.users = users;
        }

        public User Execute(TIdentifier userId)
        {
            User user = users
                .Include(u => u.Accounts)
                .Include(u => u.Roles.Select(r => r.Permissions))
                .Include(u => u.Groups)
                .Include(u => u.Organizations)
                .Include(u => u.CustomProperties)
                .SingleOrDefault(GetUserIdentifierMatchesPredicate(userId));

            return user;
        }

        protected abstract Expression<Func<User, bool>> GetUserIdentifierMatchesPredicate(TIdentifier userId);
    }
}