using System;

namespace Affecto.IdentityManagement.Querying.Data
{
    public class Organization
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public bool IsDisabled { get; set; }
    }
}