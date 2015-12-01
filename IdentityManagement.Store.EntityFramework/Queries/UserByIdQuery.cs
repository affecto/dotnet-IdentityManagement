using System;
using System.Linq;
using System.Linq.Expressions;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal class UserByIdQuery : UserQuery<Guid>
    {
        public UserByIdQuery(IQueryable<User> users)
            : base(users)
        {
        }

        protected override Expression<Func<User, bool>> GetUserIdentifierMatchesPredicate(Guid userId)
        {
            return user => user.Id == userId;
        }
    }
}