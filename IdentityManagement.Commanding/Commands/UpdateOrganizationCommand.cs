using System;

namespace Affecto.IdentityManagement.Commanding.Commands
{
    public class UpdateOrganizationCommand : IIdAndNameCommand
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Email { get; private set; }
        public bool IsDisabled { get; private set; }

        public UpdateOrganizationCommand(Guid id, string name, string description, string email, bool isDisabled)
        {
            Id = id;
            Name = name;
            Description = description;
            Email = email;
            IsDisabled = isDisabled;
        }
    }
}