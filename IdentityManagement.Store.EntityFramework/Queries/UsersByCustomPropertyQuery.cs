using System.Data.Entity;
using System.Linq;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal class UsersByCustomPropertyQuery
    {
        private readonly IQueryable<User> users;

        public UsersByCustomPropertyQuery(IQueryable<User> users)
        {
            this.users = users;
        }

        public IQueryable<User> Execute(string customPropertyName, string customPropertyValue)
        {
            return users
                .Include(u => u.Accounts)
                .Include(u => u.Roles.Select(r => r.Permissions))
                .Include(u => u.Groups)
                .Include(u => u.Organizations)
                .Include(u => u.CustomProperties)
                .Where(u => u.CustomProperties.Any(c => c.Name == customPropertyName && c.Value == customPropertyValue));
        }
    }
}