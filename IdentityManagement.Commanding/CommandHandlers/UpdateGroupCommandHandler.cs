using System;
using Affecto.IdentityManagement.Commanding.Commands;
using Affecto.IdentityManagement.Commanding.Validation;
using Affecto.IdentityManagement.Interfaces.Exceptions;
using Affecto.IdentityManagement.Store.Model;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.CommandHandlers
{
    internal class UpdateGroupCommandHandler : ICommandHandler<UpdateGroupCommand>
    {
        private readonly IDbRepository repository;
        private readonly IAuditTrailWriter auditTrail;

        public UpdateGroupCommandHandler(IDbRepository repository, IAuditTrailWriter auditTrail)
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

        public void Execute(UpdateGroupCommand command)
        {
            IdAndNameSpecification specification = new IdAndNameSpecification();
            if (specification.IsSatisfiedBy(command))
            {
                Group group = repository.GetGroup(command.Id);

                ValidateNameIfChanged(command.Name, group);

                group.Name = command.Name;
                group.Description = command.Description;
                group.IsDisabled = command.IsDisabled;
                group.ExternalGroupName = command.ExternalGroupName;
                repository.SaveChanges();

                auditTrail.AddEntry(command.Id, command.Name, "Ryhmä päivitetty");
            }
            else
            {
                throw new ArgumentException(specification.GetReasonsForDissatisfactionSeparatedWithNewLine());
            }
        }

        private void ValidateNameIfChanged(string name, Group group)
        {
            if (name != group.Name && repository.GroupExists(name))
            {
                throw new GroupWithSameNameAlreadyExistsException(name);
            }
        }
    }
}