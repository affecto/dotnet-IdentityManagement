using System;
using Affecto.IdentityManagement.Commanding.Commands;
using Affecto.IdentityManagement.Store.Model;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.CommandHandlers
{
    internal class RemoveUserOrganizationCommandHandler : ICommandHandler<RemoveUserOrganizationCommand>
    {
        private readonly IDbRepository repository;
        private readonly IAuditTrailWriter auditTrail;

        public RemoveUserOrganizationCommandHandler(IDbRepository repository, IAuditTrailWriter auditTrail)
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

        public void Execute(RemoveUserOrganizationCommand command)
        {
            User user = repository.GetUser(command.UserId);
            if (user.IsInOrganization(command.OrganizationId))
            {
                Organization organization = repository.GetOrganization(command.OrganizationId);
                user.RemoveFromOrganization(organization);
                repository.SaveChanges();

                auditTrail.AddEntry(command.UserId, user.Name, organization.Name, "Käyttäjän organisaatio poistettu");
            }
        }
    }
}