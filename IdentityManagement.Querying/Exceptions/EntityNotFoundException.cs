using System;

namespace Affecto.IdentityManagement.Querying.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string type, Guid id)
            : base(string.Format("{0} entity not found with identifier {1}", type, id))
        {
        }

        public EntityNotFoundException(string type, string id)
            : base(string.Format("{0} entity not found with identifier {1}", type, id))
        {
        }

        public EntityNotFoundException(string message)
            : base(message)
        {
        }
    }
}