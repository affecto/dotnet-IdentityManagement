using System;
using System.Data.Entity;
using System.Linq;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal class UserQueryBuilder
    {
        private readonly IQueryable<User> users;

        public UserQueryBuilder(IQueryable<User> users)
        {
            if (users == null)
            {
                throw new ArgumentNullException("users");
            }
            this.users = users;
        }

        public IQueryable<User> IncludeAll()
        {
            return users
                .Include(u => u.Accounts)
                .Include(u => u.Roles.Select(r => r.Permissions))
                .Include(u => u.Groups)
                .Include(u => u.Organizations)
                .Include(u => u.CustomProperties);
        }
    }
}
