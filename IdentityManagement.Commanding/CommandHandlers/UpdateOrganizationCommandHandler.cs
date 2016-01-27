using System;
using Affecto.IdentityManagement.Commanding.Commands;
using Affecto.IdentityManagement.Commanding.Validation;
using Affecto.IdentityManagement.Interfaces.Exceptions;
using Affecto.IdentityManagement.Store.Model;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.CommandHandlers
{
    internal class UpdateOrganizationCommandHandler : ICommandHandler<UpdateOrganizationCommand>
    {
        private readonly IDbRepository repository;
        private readonly IAuditTrailWriter auditTrail;

        public UpdateOrganizationCommandHandler(IDbRepository repository, IAuditTrailWriter auditTrail)
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

        public void Execute(UpdateOrganizationCommand command)
        {
            IdAndNameSpecification specification = new IdAndNameSpecification();
            if (specification.IsSatisfiedBy(command))
            {
                Organization organization = repository.GetOrganization(command.Id);

                ValidateNameIfChanged(command.Name, organization);

                organization.Name = command.Name;
                organization.Description = command.Description;
                organization.Email = command.Email;
                organization.IsDisabled = command.IsDisabled;
                repository.SaveChanges();

                auditTrail.AddEntry(command.Id, command.Name, "Organisaatio päivitetty");
            }
            else
            {
                throw new ArgumentException(specification.GetReasonsForDissatisfactionSeparatedWithNewLine());
            }
        }

        private void ValidateNameIfChanged(string name, Organization organization)
        {
            if (name != organization.Name && repository.OrganizationExists(name))
            {
                throw new OrganizationWithSameNameAlreadyExistsException(name);
            }
        }
    }
}