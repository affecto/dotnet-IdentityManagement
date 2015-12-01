using System;
using System.Linq;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal class OrganizationExistsQuery
    {
        private readonly IQueryable<Organization> organizations;

        public OrganizationExistsQuery(IQueryable<Organization> organizations)
        {
            if (organizations == null)
            {
                throw new ArgumentNullException("organizations");
            }
            this.organizations = organizations;
        }

        public bool Execute(string name)
        {
            name = name.ToLower();
            return organizations.Any(o => o.Name.ToLower() == name);
        }
    }
}