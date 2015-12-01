using System;
using System.Linq;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal class GroupExistsQuery
    {
        private readonly IQueryable<Group> groups;

        public GroupExistsQuery(IQueryable<Group> groups)
        {
            if (groups == null)
            {
                throw new ArgumentNullException("groups");
            }
            this.groups = groups;
        }

        public bool Execute(string name)
        {
            name = name.ToLower();
            return groups.Any(o => o.Name.ToLower() == name);
        }
    }
}