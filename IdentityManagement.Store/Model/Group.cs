using System;
using System.Collections.Generic;
using System.Linq;

namespace Affecto.IdentityManagement.Store.Model
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDisabled { get; set; }
        public string ExternalGroupName { get; set; }

        public virtual List<User> Users { get; set; }

        public Group()
        {
            Users = new List<User>();
        }

        public bool HasUser(Guid userId)
        {
            return Users.Any(u => u.Id.Equals(userId));
        }

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public void RemoveUser(Guid userId)
        {
            Users.RemoveAll(u => u.Id.Equals(userId));
        }
    }
}