using System;

namespace Affecto.IdentityManagement.Commanding.Commands
{
    public class CreateRoleCommand : IIdAndNameCommand
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ExternalGroupName { get; private set; }

        public CreateRoleCommand(Guid id, string name, string description, string externalGroupName)
        {
            Id = id;
            Name = name;
            Description = description;
            ExternalGroupName = externalGroupName;
        }
    }
}