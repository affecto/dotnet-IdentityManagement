using System;
using System.Collections.Generic;

namespace Affecto.IdentityManagement.Interfaces.Model
{
    public interface IRole
    {
        Guid Id { get; }
        string Name { get; }
        IReadOnlyCollection<IPermission> Permissions { get; }
        string Description { get; }
        string ExternalGroupName { get; }
    }
}