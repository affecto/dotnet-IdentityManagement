using System;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.Commands
{
    public class AddRolePermissionCommand : ICommand
    {
        public Guid RoleId { get; private set; }
        public Guid PermissionId { get; private set; }

        public AddRolePermissionCommand(Guid roleId, Guid permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }
    }
}