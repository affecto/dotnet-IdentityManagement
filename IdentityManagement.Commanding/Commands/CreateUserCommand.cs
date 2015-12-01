using System;
using System.Collections.Generic;

namespace Affecto.IdentityManagement.Commanding.Commands
{
    public class CreateUserCommand : IIdAndNameCommand
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyCollection<KeyValuePair<string, string>> CustomProperties { get; private set; }

        public CreateUserCommand(Guid id, string name, IReadOnlyCollection<KeyValuePair<string, string>> customProperties)
        {
            Id = id;
            Name = name;
            CustomProperties = customProperties;
        }
    }
}