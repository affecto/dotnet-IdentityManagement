using System;
using System.Collections.Generic;

namespace Affecto.IdentityManagement.WebApi.Model
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExternalGroupName { get; set; }
        public ICollection<Permission> Permissions { get; set; }
    }
}