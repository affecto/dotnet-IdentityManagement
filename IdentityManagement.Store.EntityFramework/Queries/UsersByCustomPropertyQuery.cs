using System.Linq;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal class UsersByCustomPropertyQuery
    {
        private readonly UserQueryBuilder queryBuilder;

        public UsersByCustomPropertyQuery(IQueryable<User> users)
        {
            queryBuilder = new UserQueryBuilder(users);
        }

        public IQueryable<User> Execute(string customPropertyName, string customPropertyValue)
        {
            return queryBuilder.IncludeAll()
                .Where(u => u.CustomProperties.Any(c => c.Name == customPropertyName && c.Value == customPropertyValue));
        }
    }
}