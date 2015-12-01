using System;
using Affecto.IdentityManagement.Commanding.Commands;
using Affecto.IdentityManagement.Commanding.Validation;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.CommandHandlers
{
    internal class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IDbRepository repository;
        private readonly IAuditTrailWriter auditTrail;

        public CreateUserCommandHandler(IDbRepository repository, IAuditTrailWriter auditTrail)
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

        public void Execute(CreateUserCommand command)
        {
            IdAndNameSpecification specification = new IdAndNameSpecification();
            if (specification.IsSatisfiedBy(command))
            {
                repository.AddUser(command.Id, command.Name, command.CustomProperties);
                repository.SaveChanges();

                auditTrail.AddEntry(command.Id, command.Name, "Käyttäjä luotu");
            }
            else
            {
                throw new ArgumentException(specification.GetReasonsForDissatisfactionSeparatedWithNewLine());
            }
        }
    }
}