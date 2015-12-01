using System;
using System.Runtime.Serialization;

namespace Affecto.IdentityManagement.Interfaces.Exceptions
{
    [Serializable]
    public class UserAccountAlreadyAssignedException : Exception
    {
        public UserAccountAlreadyAssignedException(string accountName)
            : this(accountName, null)
        {
        }

        public UserAccountAlreadyAssignedException(string accountName, Exception inner)
            : base(string.Format("Account '{0}' is already assigned to another user.", accountName), inner)
        {
        }

        protected UserAccountAlreadyAssignedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}