using System;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.Commands
{
    public class RemoveGroupMemberCommand : ICommand
    {
        public Guid GroupId { get; private set; }
        public Guid UserId { get; private set; }

        public RemoveGroupMemberCommand(Guid groupId, Guid userId)
        {
            GroupId = groupId;
            UserId = userId;
        }
    }
}