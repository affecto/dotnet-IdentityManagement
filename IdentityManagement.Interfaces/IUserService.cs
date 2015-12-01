using System;
using System.Collections.Generic;
using Affecto.IdentityManagement.Interfaces.Model;

namespace Affecto.IdentityManagement.Interfaces
{
    public interface IUserService
    {
        bool IsExistingUserAccount(string accountName, AccountType accountType);
        IUser GetUser(string accountName, AccountType accountType);
        Guid AddUser(string accountName, AccountType type, string displayName, IEnumerable<string> authenticatedGroups);
        void UpdateUserGroupsAndRoles(string accountName, AccountType accountType, IEnumerable<string> authenticatedGroups);
        bool IsMatchingPassword(string accountName, string password);
    }
}