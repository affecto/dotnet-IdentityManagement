using System;
using System.Runtime.Serialization;
using Affecto.IdentityManagement.Interfaces.Model;

namespace Affecto.IdentityManagement.Interfaces.Exceptions
{
    [Serializable]
    public class UserAccountTypeAlreadyAssignedException : Exception
    {
        public UserAccountTypeAlreadyAssignedException(AccountType accountType, Exception inner = null)
            : base(string.Format("Account type '{0}' is already assigned for user.", accountType), inner)
        {
        }

        protected UserAccountTypeAlreadyAssignedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}