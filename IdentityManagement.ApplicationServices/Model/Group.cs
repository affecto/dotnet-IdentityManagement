using System;
using Affecto.IdentityManagement.Interfaces.Model;

namespace Affecto.IdentityManagement.ApplicationServices.Model
{
    internal class Group : IGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDisabled { get; set; }
        public string ExternalGroupName { get; set; }
    }
}