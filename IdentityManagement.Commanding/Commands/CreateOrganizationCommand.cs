using System;

namespace Affecto.IdentityManagement.Commanding.Commands
{
    public class CreateOrganizationCommand : IIdAndNameCommand
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Email { get; set; }

        public CreateOrganizationCommand(Guid id, string name, string description, string email)
        {
            Id = id;
            Name = name;
            Description = description;
            Email = email;
        }
    }
}