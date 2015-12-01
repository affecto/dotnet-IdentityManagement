using System;
using System.Linq;
using Affecto.IdentityManagement.Querying.Exceptions;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal class GroupQuery
    {
        private readonly IQueryable<Group> groups;

        public GroupQuery(IQueryable<Group> groups)
        {
            this.groups = groups;
        }

        public Group Execute(Guid groupId)
        {
            Group group = groups.SingleOrDefault(g => g.Id == groupId);
            if (group == null)
            {
                throw new EntityNotFoundException("Group", groupId);
            }
            return group;
        }
    }
}