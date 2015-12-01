using System;
using Affecto.IdentityManagement.Commanding.Commands;
using Affecto.IdentityManagement.Store.Model;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.CommandHandlers
{
    internal class AddUserRoleCommandHandler : ICommandHandler<AddUserRoleCommand>
    {
        private readonly IDbRepository repository;
        private readonly IAuditTrailWriter auditTrail;

        public AddUserRoleCommandHandler(IDbRepository repository, IAuditTrailWriter auditTrail)
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

        public void Execute(AddUserRoleCommand command)
        {
            User user = repository.GetUserWithRoles(command.UserId);
            if (!user.HasRole(command.RoleId))
            {
                Role role = repository.GetRole(command.RoleId);
                user.AddRole(role);
                repository.SaveChanges();

                auditTrail.AddEntry(command.RoleId, user.Name, role.Name, "Käyttäjä lisätty rooliin");
            }
        }
    }
}