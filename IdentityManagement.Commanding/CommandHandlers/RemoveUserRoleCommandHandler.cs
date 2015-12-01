using System;
using Affecto.IdentityManagement.Commanding.Commands;
using Affecto.IdentityManagement.Store.Model;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.CommandHandlers
{
    internal class RemoveUserRoleCommandHandler : ICommandHandler<RemoveUserRoleCommand>
    {
        private readonly IDbRepository repository;
        private readonly IAuditTrailWriter auditTrail;

        public RemoveUserRoleCommandHandler(IDbRepository repository, IAuditTrailWriter auditTrail)
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

        public void Execute(RemoveUserRoleCommand command)
        {
            User user = repository.GetUserWithRoles(command.UserId);
            if (user.HasRole(command.RoleId))
            {
                Role role = user.GetRole(command.RoleId);
                user.RemoveRole(command.RoleId);
                repository.SaveChanges();

                auditTrail.AddEntry(command.UserId, user.Name, role.Name, "Käyttäjän rooli poistettu");
            }
        }
    }
}