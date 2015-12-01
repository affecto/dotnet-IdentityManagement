using System;
using System.Linq;
using Affecto.IdentityManagement.Commanding.Commands;
using Affecto.Patterns.Specification;

namespace Affecto.IdentityManagement.Commanding.Validation
{
    internal class UserIdAndNameSpecification : Specification<IUserIdAndNameCommand>
    {
        protected override bool IsSatisfied(IUserIdAndNameCommand command)
        {
            if (command.UserId.Equals(Guid.Empty))
            {
                AddReasonForDissatisfaction("UserId must be defined.");
            }
            if (string.IsNullOrWhiteSpace(command.Name))
            {
                AddReasonForDissatisfaction("Name must be defined.");
            }
            return !ReasonsForDissatisfaction.Any();
        }
    }
}