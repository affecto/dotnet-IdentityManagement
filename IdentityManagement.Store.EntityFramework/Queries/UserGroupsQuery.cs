using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal class UserGroupsQuery
    {
        private readonly IQueryable<User> users;

        public UserGroupsQuery(IQueryable<User> users)
        {
            this.users = users;
        }

        public IEnumerable<Group> Execute(Guid userId)
        {
            return users.Include(u => u.Groups).Where(u => u.Id == userId).Select(u => u.Groups).SingleOrDefault();
        }
    }
}