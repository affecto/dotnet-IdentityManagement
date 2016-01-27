using System;
using Affecto.IdentityManagement.Commanding.Commands;
using Affecto.IdentityManagement.Commanding.Validation;
using Affecto.IdentityManagement.Interfaces.Exceptions;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.CommandHandlers
{
    internal class CreateOrganizationCommandHandler : ICommandHandler<CreateOrganizationCommand>
    {
        private readonly IDbRepository repository;
        private readonly IAuditTrailWriter auditTrail;

        public CreateOrganizationCommandHandler(IDbRepository repository, IAuditTrailWriter auditTrail)
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

        public void Execute(CreateOrganizationCommand command)
        {
            IdAndNameSpecification specification = new IdAndNameSpecification();
            if (specification.IsSatisfiedBy(command))
            {
                if (repository.OrganizationExists(command.Name))
                {
                    throw new OrganizationWithSameNameAlreadyExistsException(command.Name);
                }

                repository.AddOrganization(command.Id, command.Name, command.Description);
                repository.SaveChanges();

                auditTrail.AddEntry(command.Id, command.Name, "Organisaatio luotu");
            }
            else
            {
                throw new ArgumentException(specification.GetReasonsForDissatisfactionSeparatedWithNewLine());
            }
        }
    }
}