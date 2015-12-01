using System;
using Affecto.IdentityManagement.Commanding.Commands;
using Affecto.IdentityManagement.Commanding.Validation;
using Affecto.IdentityManagement.Interfaces.Exceptions;
using Affecto.IdentityManagement.Store.Model;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.CommandHandlers
{
    internal class UpdateRoleCommandHandler : ICommandHandler<UpdateRoleCommand>
    {
        private readonly IDbRepository repository;
        private readonly IAuditTrailWriter auditTrail;

        public UpdateRoleCommandHandler(IDbRepository repository, IAuditTrailWriter auditTrail)
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

        public void Execute(UpdateRoleCommand command)
        {
            IdAndNameSpecification specification = new IdAndNameSpecification();
            if (specification.IsSatisfiedBy(command))
            {
                Role role = repository.GetRole(command.Id);

                ValidateNameIfChanged(command.Name, role);

                role.Name = command.Name;
                role.Description = command.Description;
                role.ExternalGroupName = command.ExternalGroupName;

                repository.SaveChanges();

                auditTrail.AddEntry(command.Id, command.Name, "Rooli päivitetty");
            }
            else
            {
                throw new ArgumentException(specification.GetReasonsForDissatisfactionSeparatedWithNewLine());
            }
        }

        private void ValidateNameIfChanged(string name, Role role)
        {
            if (name != role.Name && repository.RoleExists(name))
            {
                throw new RoleWithSameNameAlreadyExistsException(name);
            }
        }
    }
}