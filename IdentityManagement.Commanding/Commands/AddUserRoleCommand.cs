using System;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.Commands
{
    public class AddUserRoleCommand : ICommand
    {
        public Guid UserId { get; private set; }
        public Guid RoleId { get; private set; }

        public AddUserRoleCommand(Guid userId, Guid roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}