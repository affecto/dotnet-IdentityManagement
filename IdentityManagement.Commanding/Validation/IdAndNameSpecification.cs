using System;
using System.Linq;
using Affecto.IdentityManagement.Commanding.Commands;
using Affecto.Patterns.Specification;

namespace Affecto.IdentityManagement.Commanding.Validation
{
    internal class IdAndNameSpecification : Specification<IIdAndNameCommand>
    {
        protected override bool IsSatisfied(IIdAndNameCommand command)
        {
            if (command.Id.Equals(Guid.Empty))
            {
                AddReasonForDissatisfaction("Id must be defined.");
            }
            if (string.IsNullOrWhiteSpace(command.Name))
            {
                AddReasonForDissatisfaction("Name must be defined.");
            }
            return !ReasonsForDissatisfaction.Any();
        }
    }
}