using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.IdentityManagement.Store.EntityFramework.DatabaseTests
{
    public abstract class DbRepositoryTests : DbTests
    {
        internal DbRepository sut;

        [TestInitialize]
        public void Setup()
        {
            sut = new DbRepository(dbContext);
        }
    }
}