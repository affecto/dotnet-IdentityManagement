using System;
using Affecto.IdentityManagement.Commanding.CommandHandlers;
using Affecto.IdentityManagement.Commanding.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Affecto.IdentityManagement.Commanding.Tests.CommandHandlers
{
    [TestClass]
    public class CreateRoleCommandHandlerTests
    {
        private CreateRoleCommandHandler sut;
        private IDbRepository repository;

        [TestInitialize]
        public void Setup()
        {
            repository = Substitute.For<IDbRepository>();
            var auditTrailWriter = Substitute.For<IAuditTrailWriter>();
            sut = new CreateRoleCommandHandler(repository, auditTrailWriter);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RoleCannotBeCreatedWithEmptyId()
        {
            sut.Execute(new CreateRoleCommand(Guid.Empty, "name", "description", string.Empty));
        }
    }
}