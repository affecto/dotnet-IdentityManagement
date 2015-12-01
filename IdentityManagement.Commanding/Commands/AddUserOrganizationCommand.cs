using System;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.Commands
{
    public class AddUserOrganizationCommand : ICommand
    {
        public Guid UserId { get; private set; }
        public Guid OrganizationId { get; private set; }

        public AddUserOrganizationCommand(Guid userId, Guid organizationId)
        {
            UserId = userId;
            OrganizationId = organizationId;
        }
    }
}