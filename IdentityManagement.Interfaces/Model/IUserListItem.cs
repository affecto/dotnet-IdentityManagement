using System;

namespace Affecto.IdentityManagement.Interfaces.Model
{
    public interface IUserListItem
    {
        Guid Id { get; }
        string Name { get; }
        bool IsDisabled { get; }
    }
}