using System;
using Affecto.IdentityManagement.Store.Model;

namespace Affecto.IdentityManagement.Querying.Exceptions
{
    public class AccountNotFoundException : EntityNotFoundException
    {
        public AccountNotFoundException(Guid userId, AccountType type)
            : base("Account", string.Format("{0} and type {1}", userId, type))
        {
        }
    }
}