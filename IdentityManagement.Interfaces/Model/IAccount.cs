
namespace Affecto.IdentityManagement.Interfaces.Model
{
    public interface IAccount
    {
        AccountType Type { get; }
        string Name { get; }
    }
}