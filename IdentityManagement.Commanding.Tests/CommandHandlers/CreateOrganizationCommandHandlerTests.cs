using System;
using Affecto.IdentityManagement.Commanding.CommandHandlers;
using Affecto.IdentityManagement.Commanding.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Affecto.IdentityManagement.Commanding.Tests.CommandHandlers
{
    [TestClass]
    public class CreateOrganizationCommandHandlerTests
    {
        private CreateOrganizationCommandHandler sut;
        private IDbRepository repository;

        [TestInitialize]
        public void Setup()
        {
            repository = Substitute.For<IDbRepository>();
            var auditTrailWriter = Substitute.For<IAuditTrailWriter>();
            sut = new CreateOrganizationCommandHandler(repository, auditTrailWriter);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OrganizationCannotBeCreatedWithEmptyId()
        {
            sut.Execute(new CreateOrganizationCommand(Guid.Empty, "name", "description"));
        }
    }
}