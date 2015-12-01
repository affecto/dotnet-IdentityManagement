using System;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.Commands
{
    public class RemoveUserRoleCommand : ICommand
    {
        public Guid UserId { get; private set; }
        public Guid RoleId { get; private set; }

        public RemoveUserRoleCommand(Guid userId, Guid roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}