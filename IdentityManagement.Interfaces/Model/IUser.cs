using System.Collections.Generic;

namespace Affecto.IdentityManagement.Interfaces.Model
{
    public interface IUser: IUserListItem
    {
        IReadOnlyCollection<IAccount> Accounts { get; }
        IReadOnlyCollection<IOrganization> Organizations { get; }
        IReadOnlyCollection<IRole> Roles { get; }
        IReadOnlyCollection<IGroup> Groups { get; }
        IReadOnlyCollection<ICustomProperty> CustomProperties { get; }
    }
}