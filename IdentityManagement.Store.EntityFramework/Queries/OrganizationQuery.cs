using System;
using System.Linq;
using Affecto.IdentityManagement.Querying.Exceptions;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    public class OrganizationQuery
    {
        private readonly IQueryable<Organization> organizations;

        public OrganizationQuery(IQueryable<Organization> organizations)
        {
            if (organizations == null)
            {
                throw new ArgumentNullException("organizations");
            }
            this.organizations = organizations;
        }

        public Organization Execute(Guid organizationId)
        {
            Organization organization = organizations.SingleOrDefault(o => o.Id.Equals(organizationId));
            if (organization == null)
            {
                throw new EntityNotFoundException("Organization", organizationId);
            }
            return organization;
        }
    }
}