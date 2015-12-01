using System;
using System.Collections.Generic;
using Affecto.IdentityManagement.Interfaces.Model;

namespace Affecto.IdentityManagement.ApplicationServices.Model
{
    internal class Role : IRole
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExternalGroupName { get; set; }
        public IReadOnlyCollection<IPermission> Permissions { get; set; }

        public Role()
        {
            Permissions = new List<Permission>();
        }
    }
}