using System;

namespace Affecto.IdentityManagement.Commanding.Commands
{
    public class CreateExternalUserAccountCommand : IUserIdAndNameCommand
    {
        public Guid UserId { get; private set; }
        public Interfaces.Model.AccountType Type { get; private set; }
        public string Name { get; private set; }

        public CreateExternalUserAccountCommand(Guid userId, Interfaces.Model.AccountType type, string name)
        {
            UserId = userId;
            Type = type;
            Name = name;
        }
    }
}