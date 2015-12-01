using System;
using Affecto.IdentityManagement.Commanding.Commands;
using Affecto.IdentityManagement.Store.Model;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.CommandHandlers
{
    internal class RemoveGroupMemberCommandHandler : ICommandHandler<RemoveGroupMemberCommand>
    {
        private readonly IDbRepository repository;
        private readonly IAuditTrailWriter auditTrail;

        public RemoveGroupMemberCommandHandler(IDbRepository repository, IAuditTrailWriter auditTrail)
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

        public void Execute(RemoveGroupMemberCommand command)
        {
            Group group = repository.GetGroupWithUsers(command.GroupId);
            if (group.HasUser(command.UserId))
            {
                User user = repository.GetUser(command.UserId);
                group.RemoveUser(command.UserId);
                repository.SaveChanges();

                auditTrail.AddEntry(command.UserId, group.Name, user.Name, "Ryhmän käyttäjä poistettu");
            }
        }
    }
}