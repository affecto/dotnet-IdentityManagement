using System;
using System.Linq;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal class RoleExistsQuery
    {
        private readonly IQueryable<Role> roles;

        public RoleExistsQuery(IQueryable<Role> roles)
        {
            if (roles == null)
            {
                throw new ArgumentNullException("roles");
            }
            this.roles = roles;
        }

        public bool Execute(string name)
        {
            name = name.ToLower();
            return roles.Any(o => o.Name.ToLower() == name);
        }
    }
}