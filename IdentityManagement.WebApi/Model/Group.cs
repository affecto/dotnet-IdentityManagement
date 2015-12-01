using System;

namespace Affecto.IdentityManagement.WebApi.Model
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
        public string Description { get; set; }
        public string ExternalGroupName { get; set; }
    }
}