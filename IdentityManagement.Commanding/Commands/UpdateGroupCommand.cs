using System;

namespace Affecto.IdentityManagement.Commanding.Commands
{
    public class UpdateGroupCommand : IIdAndNameCommand
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsDisabled { get; private set; }
        public string ExternalGroupName { get; private set; }

        public UpdateGroupCommand(Guid id, string name, string description, bool isDisabled, string externalGroupName)
        {
            Id = id;
            Name = name;
            Description = description;
            IsDisabled = isDisabled;
            ExternalGroupName = externalGroupName;
        }
    }
}