using System;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.Commands
{
    public class AddGroupMemberCommand : ICommand
    {
        public Guid GroupId { get; private set; }
        public Guid UserId { get; private set; }

        public AddGroupMemberCommand(Guid groupId, Guid userId)
        {
            GroupId = groupId;
            UserId = userId;
        }
    }
}