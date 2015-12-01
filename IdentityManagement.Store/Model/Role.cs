using System;
using System.Collections.Generic;
using System.Linq;

namespace Affecto.IdentityManagement.Store.Model
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExternalGroupName { get; set; }

        public virtual List<Permission> Permissions { get; set; }

        public Role()
        {
            Permissions = new List<Permission>();
        }

        public bool HasPermission(Guid permissionId)
        {
            return Permissions.Any(o => o.Id.Equals(permissionId));
        }

        public void AddPermission(Permission permission)
        {
            Permissions.Add(permission);
        }

        public void RemovePermission(Guid id)
        {
            Permissions.RemoveAll(o => o.Id.Equals(id));
        }
    }
}