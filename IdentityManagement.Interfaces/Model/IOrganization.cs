using System;

namespace Affecto.IdentityManagement.Interfaces.Model
{
    public interface IOrganization
    {
        Guid Id { get; }
        string Name { get; }
        string Description { get; }
        string Email { get; set; }
        bool IsDisabled { get; set; }
    }
}