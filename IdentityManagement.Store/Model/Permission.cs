using System;
using System.Collections.Generic;

namespace Affecto.IdentityManagement.Store.Model
{
    public class Permission
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<Role> Roles { get; set; }

        public Permission()
        {
            Roles = new List<Role>();
        }
    }
}