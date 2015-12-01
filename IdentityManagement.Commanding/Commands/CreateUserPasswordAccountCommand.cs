using System;

namespace Affecto.IdentityManagement.Commanding.Commands
{
    public class CreateUserPasswordAccountCommand : IUserIdAndNameCommand
    {
        public Guid UserId { get; private set; }
        public string Name { get; private set; }
        public string Password { get; private set; }

        public CreateUserPasswordAccountCommand(Guid userId, string name, string password)
        {
            UserId = userId;
            Name = name;
            Password = password;
        }
    }
}