using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Affecto.IdentityManagement.Interfaces;
using Affecto.IdentityManagement.Interfaces.Model;
using Affecto.IdentityManagement.WebApi.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Affecto.IdentityManagement.WebApi.Tests
{
    [TestClass]
    public class IdentityManagementControllerTests
    {
        private IdentityManagementController sut;
        IIdentityManagementService identityManagementService;

        [TestInitialize]
        public void Setup()
        {
            identityManagementService = Substitute.For<IIdentityManagementService>();
            sut = new IdentityManagementController(identityManagementService);
        }

        [TestMethod]
        public void GetGroup()
        {
            IGroup group = Substitute.For<IGroup>();
            group.IsDisabled.Returns(true);
            group.Description.Returns("MyDescription");
            group.Name.Returns("GroupName");
            identityManagementService.GetGroup(Arg.Any<Guid>()).Returns(group);

            OkNegotiatedContentResult<Group> response = sut.GetGroup(Guid.NewGuid()) as OkNegotiatedContentResult<Group>;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            Assert.IsTrue(response.Content.IsDisabled);
            Assert.AreEqual("MyDescription", response.Content.Description);
            Assert.AreEqual("GroupName", response.Content.Name);
        }

        [TestMethod]
        public void GetOrganization()
        {
            IOrganization organization = Substitute.For<IOrganization>();
            organization.IsDisabled.Returns(true);
            organization.Name.Returns("OrganizationName");
            organization.Description.Returns("MyDescription");
            identityManagementService.GetOrganization(Arg.Any<Guid>()).Returns(organization);

            OkNegotiatedContentResult<Organization> response = sut.GetOrganization(Guid.NewGuid()) as OkNegotiatedContentResult<Organization>;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            Assert.IsTrue(response.Content.IsDisabled);
            Assert.AreEqual("OrganizationName", response.Content.Name);
            Assert.AreEqual("MyDescription", response.Content.Description);
        }

        [TestMethod]
        public void GetUser()
        {
            Guid userId = Guid.NewGuid();
            IUser user = Substitute.For<IUser>();
            user.IsDisabled.Returns(true);
            user.Name.Returns("DisplayName");
            identityManagementService.GetUser(userId).Returns(user);

            ICustomProperty customProperty1 = Substitute.For<ICustomProperty>();
            customProperty1.Name.Returns("prop1");
            customProperty1.Value.Returns("value1");
            ICustomProperty customProperty2 = Substitute.For<ICustomProperty>();
            customProperty2.Name.Returns("prop2");
            customProperty2.Value.Returns((string) null);

            user.CustomProperties.Returns(new List<ICustomProperty> { customProperty1, customProperty2 });

            OkNegotiatedContentResult<User> response = sut.GetUser(userId) as OkNegotiatedContentResult<User>;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            Assert.IsTrue(response.Content.IsDisabled);
            Assert.AreEqual("DisplayName", response.Content.Name);

            Assert.AreEqual(2, response.Content.CustomProperties.Count);

            CustomProperty customProperty = response.Content.CustomProperties.SingleOrDefault(c => c.Name == "prop1");
            Assert.IsNotNull(customProperty);
            Assert.AreEqual("value1", customProperty.Value);

            customProperty = response.Content.CustomProperties.SingleOrDefault(c => c.Name == "prop2");
            Assert.IsNotNull(customProperty);
            Assert.IsNull(customProperty.Value);
        }

        [TestMethod]
        public void AddExternalUserAccount()
        {
            Guid userId = Guid.NewGuid();

            var command = new AddUserAccountCommand
            {
                Name = "someUser",
                Type = "ActiveDirectory"
            };

            var result = sut.AddUserAccount(userId, command) as OkResult;

            Assert.IsNotNull(result);
            identityManagementService.Received(1).AddExternalUserAccount(userId, AccountType.ActiveDirectory, command.Name);
        }

        [TestMethod]
        public void AddPasswordUserAccount()
        {
            Guid userId = Guid.NewGuid();

            var command = new AddUserAccountCommand
            {
                Name = "someUser",
                Password = "sala",
                Type = "Password"
            };

            var result = sut.AddUserAccount(userId, command) as OkResult;

            Assert.IsNotNull(result);
            identityManagementService.Received(1).AddUserAccount(userId, command.Name, command.Password);
        }
    }
}