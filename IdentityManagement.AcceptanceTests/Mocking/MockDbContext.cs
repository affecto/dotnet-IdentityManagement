using Affecto.IdentityManagement.Store.EntityFramework;

namespace Affecto.IdentityManagement.AcceptanceTests.Mocking
{
    internal class MockDbContext : DbContext
    {
        public MockDbContext()
            : base(Effort.DbConnectionFactory.CreateTransient())
        {
        }
    }
}