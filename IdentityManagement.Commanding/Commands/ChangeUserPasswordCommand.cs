using System;
using Affecto.Authentication.Passwords;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.Commands
{
    public class ChangeUserPasswordCommand : ICommand
    {
        public Guid Id { get; private set; }
        public string Password { get; private set; }

        public ChangeUserPasswordCommand(Guid id, string password)
        {
            Id = id;
            Password = password;
        }
    }
}