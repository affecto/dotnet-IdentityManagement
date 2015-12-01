using System;

namespace Affecto.IdentityManagement.Commanding.Commands
{
    public class UpdateUserCommand : IIdAndNameCommand
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool IsDisabled { get; private set; }

        public UpdateUserCommand(Guid id, string name, bool isDisabled)
        {
            Id = id;
            Name = name;
            IsDisabled = isDisabled;
        }
    }
}