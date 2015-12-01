using System;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.Commands
{
    internal interface IIdAndNameCommand : ICommand
    {
        Guid Id { get; }
        string Name { get; }
    }
}