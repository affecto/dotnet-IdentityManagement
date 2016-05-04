using System.Linq;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework.Queries
{
    internal class UsersByCustomPropertyAndAccountTypeQuery : UsersByCustomPropertyQuery
    {
        public UsersByCustomPropertyAndAccountTypeQuery(IQueryable<User> users)
            : base(users)
        {
        }

        public IQueryable<User> Execute(string customPropertyName, string customPropertyValue, AccountType accountType)
        {
            return Execute(customPropertyName, customPropertyValue)
                .Where(u => u.Accounts.Any(a => a.Type == accountType));
        }
    }
}
