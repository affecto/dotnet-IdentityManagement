using System;
using Affecto.IdentityManagement.Store.Model;
using Affecto.IdentityManagement.Store.PostgreSql;

namespace Affecto.IdentityManagement.Store.Mocking
{
    internal class MockPostgreDbContext : PostgreDbContext
    {
        public MockPostgreDbContext()
            : base(Effort.DbConnectionFactory.CreateTransient())
        {
            AddInitialRoles();
        }

        private void AddInitialRoles()
        {
            Roles.Add(new Role { Id = Guid.NewGuid(), Name = "PTV-pääkäyttäjä" });
            Roles.Add(new Role { Id = Guid.NewGuid(), Name = "Organisaation pääkäyttäjä" });
            Roles.Add(new Role { Id = Guid.NewGuid(), Name = "Ylläpitäjä" });
            SaveChanges();
        }
    }
}