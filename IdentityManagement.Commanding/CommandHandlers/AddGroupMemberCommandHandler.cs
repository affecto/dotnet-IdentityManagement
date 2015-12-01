using System;
using Affecto.IdentityManagement.Commanding.Commands;
using Affecto.IdentityManagement.Store.Model;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.CommandHandlers
{
    internal class AddGroupMemberCommandHandler : ICommandHandler<AddGroupMemberCommand>
    {
        private readonly IDbRepository repository;
        private readonly IAuditTrailWriter auditTrail;

        public AddGroupMemberCommandHandler(IDbRepository repository, IAuditTrailWriter auditTrail)
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

        public void Execute(AddGroupMemberCommand command)
        {
            Group group = repository.GetGroupWithUsers(command.GroupId);
            if (!group.HasUser(command.UserId))
            {
                User user = repository.GetUser(command.UserId);
                group.AddUser(user);
                repository.SaveChanges();

                auditTrail.AddEntry(command.GroupId, user.Name, group.Name, "Käyttäjä lisätty ryhmään");
            }
        }
    }
}