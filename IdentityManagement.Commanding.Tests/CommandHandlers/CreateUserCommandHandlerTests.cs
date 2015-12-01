using System;
using Affecto.IdentityManagement.Commanding.CommandHandlers;
using Affecto.IdentityManagement.Commanding.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Affecto.IdentityManagement.Commanding.Tests.CommandHandlers
{
    [TestClass]
    public class CreateUserCommandHandlerTests
    {
        private CreateUserCommandHandler sut;
        private IDbRepository repository;

        [TestInitialize]
        public void Setup()
        {
            repository = Substitute.For<IDbRepository>();
            var auditTrailWriter = Substitute.For<IAuditTrailWriter>();
            sut = new CreateUserCommandHandler(repository, auditTrailWriter);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UserCannotBeCreatedWithEmptyId()
        {
            sut.Execute(new CreateUserCommand(Guid.Empty, "name", null));
        }
    }
}