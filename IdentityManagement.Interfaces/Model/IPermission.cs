using System;

namespace Affecto.IdentityManagement.Interfaces.Model
{
    public interface IPermission
    {
        Guid Id { get; }
        string Name { get; }
        string Description { get; }
    }
}