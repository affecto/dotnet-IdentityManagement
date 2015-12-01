using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.IdentityManagement.Store.EntityFramework.DatabaseTests
{
    public abstract class DbQueryServiceTests : DbTests
    {
        internal QueryService sut;

        [TestInitialize]
        public void CreateRepository()
        {
            sut = new QueryService(dbContext);
        }
    }
}