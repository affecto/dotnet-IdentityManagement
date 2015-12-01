using System;
using Affecto.IdentityManagement.Interfaces.Model;

namespace Affecto.IdentityManagement.ApplicationServices.Model
{
    internal class UserListItem : IUserListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
    }
}