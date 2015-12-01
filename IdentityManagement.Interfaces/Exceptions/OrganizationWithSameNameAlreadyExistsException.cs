using System;
using System.Runtime.Serialization;

namespace Affecto.IdentityManagement.Interfaces.Exceptions
{
    [Serializable]
    public class OrganizationWithSameNameAlreadyExistsException : Exception
    {
        public OrganizationWithSameNameAlreadyExistsException(string name)
            : base(string.Format("Organization with name '{0}' already exists.", name))
        {
        }

        protected OrganizationWithSameNameAlreadyExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}