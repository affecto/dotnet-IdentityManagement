using System;
using System.Data.Entity;
using System.Linq;
using Affecto.IdentityManagement.Querying.Exceptions;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal class GroupWithUsersQuery
    {
        private readonly IQueryable<Group> groups;

        public GroupWithUsersQuery(IQueryable<Group> groups)
        {
            this.groups = groups;
        }

        public Group Execute(Guid groupId)
        {
            Group group = groups.Include(u => u.Users).SingleOrDefault(g => g.Id == groupId);
            if (group == null)
            {
                throw new EntityNotFoundException("Group", groupId);
            }
            return group;
        }
    }
}