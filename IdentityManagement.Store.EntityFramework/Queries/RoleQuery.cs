using System;
using System.Linq;
using Affecto.IdentityManagement.Querying.Exceptions;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal class RoleQuery
    {
        private readonly IQueryable<Role> roles;

        public RoleQuery(IQueryable<Role> roles)
        {
            this.roles = roles;
        }

        public Role Execute(Guid roleId)
        {
            Role role = roles.SingleOrDefault(r => r.Id == roleId);
            if (role == null)
            {
                throw new EntityNotFoundException("Role", roleId);
            }
            return role;
        }
    }
}