using System;

namespace Affecto.IdentityManagement.WebApi.Model
{
    public class Organization
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
        public string Description { get; set; }
    }
}