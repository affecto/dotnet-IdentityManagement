using System;
using System.Collections.Generic;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.Commands
{
    public class UpdateUserCustomPropertiesCommand :  ICommand
    {
        public Guid Id { get; private set; }
        
        public IReadOnlyCollection<KeyValuePair<string, string>> CustomProperties { get; private set; }

        public UpdateUserCustomPropertiesCommand(Guid id,  IReadOnlyCollection<KeyValuePair<string, string>> customProperties)
        {
            Id = id;
            CustomProperties = customProperties;
        }
    }
}