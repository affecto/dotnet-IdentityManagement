using System;
using System.Linq;
using Affecto.IdentityManagement.Querying.Exceptions;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal class PermissionQuery
    {
        private readonly IQueryable<Permission> permissions;

        public PermissionQuery(IQueryable<Permission> permissions)
        {
            this.permissions = permissions;
        }

        public Permission Execute(Guid permissionId)
        {
            Permission permission = permissions.SingleOrDefault(o => o.Id == permissionId);
            if (permission == null)
            {
                throw new EntityNotFoundException("Permission", permissionId);
            }
            return permission;
        }
    }
}