using System;
using System.Data.Entity;
using System.Linq;
using Affecto.IdentityManagement.Querying.Exceptions;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal class RoleWithPermissionsQuery
    {
        private readonly IQueryable<Role> roles;

        public RoleWithPermissionsQuery(IQueryable<Role> roles)
        {
            this.roles = roles;
        }

        public Role Execute(Guid roleId)
        {
            Role role = roles.Include(r => r.Permissions).SingleOrDefault(r => r.Id == roleId);
            if (role == null)
            {
                throw new EntityNotFoundException("Role", roleId);
            }
            return role;
        }
    }
}