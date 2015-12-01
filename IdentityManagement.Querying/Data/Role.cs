using System;
using System.Collections.Generic;

namespace Affecto.IdentityManagement.Querying.Data
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExternalGroupName { get; set; }
        public IReadOnlyCollection<Permission> Permissions { get; set; }
    }
}