using System;

namespace Affecto.IdentityManagement.Interfaces.Model
{
    public interface IOrganization
    {
        Guid Id { get; }
        string Name { get; }
        string Description { get; }
        bool IsDisabled { get; set; }
    }
}