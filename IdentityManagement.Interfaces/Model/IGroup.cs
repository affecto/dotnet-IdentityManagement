using System;

namespace Affecto.IdentityManagement.Interfaces.Model
{
    public interface IGroup
    {
        Guid Id { get; }
        string Name { get; }
        string Description { get; }
        bool IsDisabled { get; }
        string ExternalGroupName { get; }
    }
}