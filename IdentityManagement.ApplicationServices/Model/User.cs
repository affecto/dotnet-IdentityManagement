using System.Collections.Generic;
using Affecto.IdentityManagement.Interfaces.Model;

namespace Affecto.IdentityManagement.ApplicationServices.Model
{
    internal class User: UserListItem, IUser
    {
        public IReadOnlyCollection<IAccount> Accounts { get; set; }
        public IReadOnlyCollection<IOrganization> Organizations { get; set; }
        public IReadOnlyCollection<IRole> Roles { get; set; }
        public IReadOnlyCollection<IGroup> Groups { get; set; }
        public IReadOnlyCollection<ICustomProperty> CustomProperties { get; set; }
    }
}