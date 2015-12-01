using System;
using Affecto.IdentityManagement.Commanding.Commands;
using Affecto.IdentityManagement.Store.Model;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.CommandHandlers
{
    internal class AddUserOrganizationCommandHandler : ICommandHandler<AddUserOrganizationCommand>
    {
        private readonly IDbRepository repository;
        private readonly IAuditTrailWriter auditTrail;

        public AddUserOrganizationCommandHandler(IDbRepository repository, IAuditTrailWriter auditTrail)
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

        public void Execute(AddUserOrganizationCommand command)
        {
            User user = repository.GetUser(command.UserId);
            if (!user.IsInOrganization(command.OrganizationId))
            {
                Organization organization = repository.GetOrganization(command.OrganizationId);
                if (organization.IsDisabled)
                {
                    throw new ArgumentException("Adding organization failed, organization disabled");
                }
                user.IncludeInOrganization(organization);
                repository.SaveChanges();

                auditTrail.AddEntry(command.OrganizationId, user.Name, organization.Name, "Käyttäjä lisätty organisaatioon");
            }
        }
    }
}