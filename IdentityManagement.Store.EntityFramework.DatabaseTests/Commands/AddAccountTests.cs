using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using Affecto.IdentityManagement.Store.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.IdentityManagement.Store.EntityFramework.DatabaseTests.Commands
{
    [TestClass]
    public class AddAccountTests : DbRepositoryTests
    {
        Guid userId;

        [TestInitialize]
        public new void Setup()
        {
            base.Setup();

            userId = Guid.NewGuid();
            const string userName = "user";
            sut.AddUser(userId, userName);
            sut.SaveChanges();
        }

        [TestMethod]
        public void AddAccount()
        {
            const string name = "user@domain";
            var type = AccountType.ActiveDirectory;

            sut.AddUserAccount(userId, type, name);
            sut.SaveChanges();

            using (DbContext readContext = new DbContext())
            {
                Account account = readContext.Accounts.Single();

                Assert.AreEqual(userId, account.UserId);
                Assert.AreEqual(type, account.Type);
                Assert.AreEqual(name, account.Name);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddAccountWithNoName()
        {
            sut.AddUserAccount(userId, AccountType.ActiveDirectory, null);
            dbContext.SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void AddTwoAccountsWithSameTypeForOneUser()
        {
            sut.AddUserAccount(userId, AccountType.ActiveDirectory, "user@domain");
            sut.AddUserAccount(userId, AccountType.ActiveDirectory, "name@domain");
            sut.SaveChanges();
        }
    }
}