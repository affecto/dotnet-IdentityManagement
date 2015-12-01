using System;
using System.Runtime.Serialization;

namespace Affecto.IdentityManagement.Interfaces.Exceptions
{
    [Serializable]
    public class RoleWithSameNameAlreadyExistsException : Exception
    {
        public RoleWithSameNameAlreadyExistsException(string name)
            : base(string.Format("Role with name '{0}' already exists.", name))
        {
        }

        protected RoleWithSameNameAlreadyExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}