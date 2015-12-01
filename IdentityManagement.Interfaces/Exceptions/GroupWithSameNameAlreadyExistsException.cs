using System;
using System.Runtime.Serialization;

namespace Affecto.IdentityManagement.Interfaces.Exceptions
{
    [Serializable]
    public class GroupWithSameNameAlreadyExistsException : Exception
    {
        public GroupWithSameNameAlreadyExistsException(string name)
            : base(string.Format("Group with name '{0}' already exists.", name))
        {
        }

        protected GroupWithSameNameAlreadyExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}