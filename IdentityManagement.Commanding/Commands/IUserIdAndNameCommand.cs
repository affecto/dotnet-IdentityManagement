using System;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.Commands
{
    internal interface IUserIdAndNameCommand : ICommand
    {
        Guid UserId { get; }
        string Name { get; }
    }
}