using System;
using Affecto.IdentityManagement.Commanding.Commands;
using Affecto.IdentityManagement.Store.Model;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.CommandHandlers
{
    internal class AddRolePermissionCommandHandler : ICommandHandler<AddRolePermissionCommand>
    {
        private readonly IDbRepository repository;
        private readonly IAuditTrailWriter auditTrail;

        public AddRolePermissionCommandHandler(IDbRepository repository, IAuditTrailWriter auditTrail)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            if (auditTrail == null)
            {
                throw new ArgumentNullException("auditTrail");
            }
            this.repository = repository;
            this.auditTrail = auditTrail;
        }

        public void Execute(AddRolePermissionCommand command)
        {
            Role role = repository.GetRoleWithPermissions(command.RoleId);
            if (!role.HasPermission(command.PermissionId))
            {
                Permission permission = repository.GetPermission(command.PermissionId);
                role.AddPermission(permission);
                repository.SaveChanges();

                auditTrail.AddEntry(command.PermissionId, role.Name, permission.Description, "Lisätty käyttöoikeus roolille");
            }
        }
    }
}