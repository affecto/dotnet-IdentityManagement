using System;
using System.Linq;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal class UserByIdQuery
    {
        private readonly UserQueryBuilder queryBuilder;

        public UserByIdQuery(IQueryable<User> users)
        {
            queryBuilder = new UserQueryBuilder(users);
        }

        public User Execute(Guid userId)
        {
            return queryBuilder.IncludeAll()
                .SingleOrDefault(u => u.Id == userId);
        }
    }
}