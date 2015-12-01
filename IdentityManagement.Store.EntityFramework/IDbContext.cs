using System.Data.Entity;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Store.EntityFramework
{
    internal interface IDbContext 
    {
        IDbSet<Account> Accounts { get; }
        IDbSet<Group> Groups { get; }
        IDbSet<Permission> Permissions { get; }
        IDbSet<Role> Roles { get; }
        IDbSet<User> Users { get; }
        IDbSet<Organization> Organizations { get; }
        IDbSet<CustomProperty> CustomProperties { get; }

        void SaveChanges();
    }
}