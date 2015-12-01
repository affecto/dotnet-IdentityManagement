using System;

namespace Affecto.IdentityManagement.WebApi.Model
{
    public class Permission
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}