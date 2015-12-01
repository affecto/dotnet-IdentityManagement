using System;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.Commands
{
    public class RemoveRolePermissionCommand : ICommand
    {
        public Guid RoleId { get; private set; }
        public Guid PermissionId { get; private set; }

        public RemoveRolePermissionCommand(Guid roleId, Guid permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }
    }
}