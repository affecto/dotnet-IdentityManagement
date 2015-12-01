using System;
using System.Data.Entity;
using System.Linq;
using Affecto.IdentityManagement.Querying.Exceptions;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal class UserWithRolesQuery
    {
        private readonly IQueryable<User> users;

        public UserWithRolesQuery(IQueryable<User> users)
        {
            this.users = users;
        }

        public User Execute(Guid userId)
        {
            User user = users.Include(u => u.Roles.Select(r => r.Permissions)).SingleOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new EntityNotFoundException("User", userId);
            }
            return user;
        }
    }
}