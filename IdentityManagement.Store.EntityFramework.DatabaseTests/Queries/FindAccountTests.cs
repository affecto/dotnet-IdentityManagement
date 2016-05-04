using System;
using System.Linq;
using Affecto.IdentityManagement.Querying.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.IdentityManagement.Store.EntityFramework.DatabaseTests.Queries
{
    [TestClass]
    public class FindAccountTests : DbQueryServiceTests
    {
        [TestMethod]
        public void GetUserByIdWhenThereAreNoAccounts()
        {
            var accounts = sut.GetAccounts(Guid.NewGuid());

            Assert.IsFalse(accounts.Any());
        }

        [TestMethod]
        public void GetAccountByUserId()
        {
            var userId = Guid.NewGuid();
            var userName = "Mike Johnsson";
            var accountName = "dev\\johnmik";

            AddActiveDirectoryUser(userId, userName, accountName);

            var accounts = sut.GetAccounts(userId);

            Assert.AreEqual(1, accounts.Count);
            var account = accounts.First();
            Assert.AreEqual(AccountType.ActiveDirectory, account.Type);
            Assert.AreEqual(accountName, account.Name);
        }
    }
}