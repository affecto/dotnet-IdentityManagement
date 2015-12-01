using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal class RolesQuery
    {
        private readonly IQueryable<Role> roles;

        public RolesQuery(IQueryable<Role> roles)
        {
            this.roles = roles;
        }

        public IEnumerable<Role> Execute()
        {
            return roles.Include(r => r.Permissions);
        }
    }
}