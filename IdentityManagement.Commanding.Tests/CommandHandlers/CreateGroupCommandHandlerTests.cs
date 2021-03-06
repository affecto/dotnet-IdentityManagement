﻿using System;
using Affecto.IdentityManagement.Commanding.CommandHandlers;
using Affecto.IdentityManagement.Commanding.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Affecto.IdentityManagement.Commanding.Tests.CommandHandlers
{
    [TestClass]
    public class CreateGroupCommandHandlerTests
    {
        private CreateGroupCommandHandler sut;
        private IDbRepository repository;

        [TestInitialize]
        public void Setup()
        {
            repository = Substitute.For<IDbRepository>();
            var auditTrailWriter = Substitute.For<IAuditTrailWriter>();
            sut = new CreateGroupCommandHandler(repository, auditTrailWriter);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GroupCannotBeCreatedWithEmptyId()
        {
            sut.Execute(new CreateGroupCommand(Guid.Empty, "name", "description", null));
        }
    }
}