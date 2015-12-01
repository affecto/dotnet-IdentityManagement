using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.IdentityManagement.ApplicationServices;
using Affecto.IdentityManagement.Commanding.Commands;
using Affecto.IdentityManagement.Querying;
using Affecto.Patterns.Cqrs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using AccountType = Affecto.IdentityManagement.Interfaces.Model.AccountType;
using Group = Affecto.IdentityManagement.Querying.Data.Group;
using Role = Affecto.IdentityManagement.Querying.Data.Role;
using User = Affecto.IdentityManagement.Querying.Data.User;

namespace Affecto.IdentityManagement.ApplicationServicesTests
{
    [TestClass]
    public class UserServiceTests
    {
        private UserService sut;

        private const string AccountName = "test account";
        private const string DisplayName = "display name";
        private const AccountType AccountType = Interfaces.Model.AccountType.ActiveDirectory;
        private List<Group> queryServiceGroups;
        private List<Role> queryServiceRoles;
        private Lazy<ICommandBus> commandBus;
        private IQueryService queryService;

        [TestInitialize]
        public void Setup()
        {
            queryServiceGroups = new List<Group>();
            queryServiceRoles = new List<Role>();

            queryService = Substitute.For<IQueryService>();
            commandBus = new Lazy<ICommandBus>(() => Substitute.For<ICommandBus>());

            queryService.GetGroups().Returns(queryServiceGroups);
            queryService.GetRoles().Returns(queryServiceRoles);

            commandBus.Value
                .When(o => o.Send(Arg.Is<Envelope<ICommand>>(p => p.Body is CreateExternalUserAccountCommand)))
                .Do(o => MockUser(o.Arg<Envelope<ICommand>>().Body as CreateExternalUserAccountCommand));

            sut = new UserService(queryService, commandBus);
        }

        [TestMethod]
        public void AddUserCreateUserAndAccount()
        {
            Guid? userId = null;
            commandBus.Value
                .When(o => o.Send(Arg.Is<Envelope<ICommand>>(p => p.Body is CreateUserCommand)))
                .Do(o => userId = ((CreateUserCommand) o.Arg<Envelope<ICommand>>().Body).Id);

            sut.AddUser(AccountName, AccountType, DisplayName, Enumerable.Empty<string>());

            commandBus.Value.Received(1).Send(Arg.Is<Envelope<ICommand>>(o =>
                ((CreateUserCommand)o.Body).Id == userId.Value
                    && ((CreateUserCommand)o.Body).Name == DisplayName));
            commandBus.Value.Received(1).Send(Arg.Is<Envelope<ICommand>>(o =>
                ((CreateExternalUserAccountCommand)o.Body).UserId == userId.Value
                    && ((CreateExternalUserAccountCommand)o.Body).Name == AccountName
                    && ((CreateExternalUserAccountCommand)o.Body).Type == AccountType));
        }

        [TestMethod]
        public void AddUserCreatesDefaultGroups()
        {
            var userGroups = new List<string> { "GROUP1", "group2" };
            var group1 = new Group { ExternalGroupName = "group1", Id = Guid.NewGuid() };
            var group2 = new Group { ExternalGroupName = "group2", Id = Guid.NewGuid() };
            queryServiceGroups.Add(group1);
            queryServiceGroups.Add(group2);

            sut.AddUser(AccountName, AccountType, DisplayName, userGroups);

            commandBus.Value.Received(1).Send(Arg.Is<Envelope<ICommand>>(o => ((AddGroupMemberCommand)o.Body).GroupId == group1.Id));
            commandBus.Value.Received(1).Send(Arg.Is<Envelope<ICommand>>(o => ((AddGroupMemberCommand)o.Body).GroupId == group2.Id));
        }

        [TestMethod]
        public void AddUserCreatesDefaultRoles()
        {
            var userGroups = new List<string> { "group1", "GROUP2" };
            var role1 = new Role { ExternalGroupName = "group1", Id = Guid.NewGuid() };
            var role2 = new Role { ExternalGroupName = "group2", Id = Guid.NewGuid() };
            queryServiceRoles.Add(role1);
            queryServiceRoles.Add(role2);

            sut.AddUser(AccountName, AccountType, DisplayName, userGroups);

            commandBus.Value.Received(1).Send(Arg.Is<Envelope<ICommand>>(o => ((AddUserRoleCommand)o.Body).RoleId == role1.Id));
            commandBus.Value.Received(1).Send(Arg.Is<Envelope<ICommand>>(o => ((AddUserRoleCommand)o.Body).RoleId == role2.Id));
        }

        [TestMethod]
        public void AddUserHandlesEmptyUserGroup()
        {
            var userGroups = new List<string> { string.Empty };
            var group1 = new Group { ExternalGroupName = "group1", Id = Guid.NewGuid() };
            var role1 = new Role { ExternalGroupName = "group1", Id = Guid.NewGuid() };
            queryServiceGroups.Add(group1);
            queryServiceRoles.Add(role1);

            sut.AddUser(AccountName, AccountType, DisplayName, userGroups);

            commandBus.Value.DidNotReceive().Send(Arg.Is<Envelope<ICommand>>(o => o.Body is AddGroupMemberCommand));
            commandBus.Value.DidNotReceive().Send(Arg.Is<Envelope<ICommand>>(o => o.Body is AddUserRoleCommand));
        }

        [TestMethod]
        public void AddUserHandlesNullUserGroup()
        {
            var userGroups = new List<string> { null };
            var group1 = new Group { ExternalGroupName = "group1", Id = Guid.NewGuid() };
            var role1 = new Role { ExternalGroupName = "group1", Id = Guid.NewGuid() };
            queryServiceGroups.Add(group1);
            queryServiceRoles.Add(role1);

            sut.AddUser(AccountName, AccountType, DisplayName, userGroups);

            commandBus.Value.DidNotReceive().Send(Arg.Is<Envelope<ICommand>>(o => o.Body is AddGroupMemberCommand));
            commandBus.Value.DidNotReceive().Send(Arg.Is<Envelope<ICommand>>(o => o.Body is AddUserRoleCommand));
        }

        [TestMethod]
        public void UpdateUserGroupsAddsMissingGroups()
        {
            var userGroups = new List<string> { "GROUP1" };
            var group1 = new Group { ExternalGroupName = "group1", Id = Guid.NewGuid() };
            var group2 = new Group { ExternalGroupName = "group2", Id = Guid.NewGuid() };
            queryServiceGroups.Add(group1);
            queryServiceGroups.Add(group2);
            CreateUser();
            commandBus.Value.ClearReceivedCalls();

            sut.UpdateUserGroupsAndRoles(AccountName, AccountType, userGroups);

            commandBus.Value.Received(1).Send(Arg.Is<Envelope<ICommand>>(o => o.Body is AddGroupMemberCommand));
            commandBus.Value.Received(1).Send(Arg.Is<Envelope<ICommand>>(o => ((AddGroupMemberCommand)o.Body).GroupId == group1.Id));
        }

        [TestMethod]
        public void UpdateUserGroupsRemoveNotMatchingGroups()
        {
            var userGroups = new List<string> { "group1" };
            var group1 = new Group { ExternalGroupName = "group1", Id = Guid.NewGuid() };
            var group2 = new Group { ExternalGroupName = "group2", Id = Guid.NewGuid() };
            queryServiceGroups.Add(group1);
            queryServiceGroups.Add(group2);
            CreateUser();
            commandBus.Value.ClearReceivedCalls();

            sut.UpdateUserGroupsAndRoles(AccountName, AccountType, userGroups);

            commandBus.Value.Received(1).Send(Arg.Is<Envelope<ICommand>>(o => o.Body is RemoveGroupMemberCommand));
            commandBus.Value.Received(1).Send(Arg.Is<Envelope<ICommand>>(o => ((RemoveGroupMemberCommand)o.Body).GroupId == group2.Id));
        }

        [TestMethod]
        public void UpdateUserGroupsAddsMissingRoles()
        {
            var userGroups = new List<string> { "GROUP1" };
            var role1 = new Role { ExternalGroupName = "group1", Id = Guid.NewGuid() };
            var role2 = new Role { ExternalGroupName = "group2", Id = Guid.NewGuid() };
            queryServiceRoles.Add(role1);
            queryServiceRoles.Add(role2);
            CreateUser();
            commandBus.Value.ClearReceivedCalls();

            sut.UpdateUserGroupsAndRoles(AccountName, AccountType, userGroups);

            commandBus.Value.Received(1).Send(Arg.Is<Envelope<ICommand>>(o => o.Body is AddUserRoleCommand));
            commandBus.Value.Received(1).Send(Arg.Is<Envelope<ICommand>>(o => ((AddUserRoleCommand)o.Body).RoleId == role1.Id));
        }

        [TestMethod]
        public void UpdateUserGroupsRemoveNotMatchingRoles()
        {
            var userGroups = new List<string> { "group1" };
            var role1 = new Role { ExternalGroupName = "group1", Id = Guid.NewGuid() };
            var role2 = new Role { ExternalGroupName = "group2", Id = Guid.NewGuid() };
            queryServiceRoles.Add(role1);
            queryServiceRoles.Add(role2);
            CreateUser();
            commandBus.Value.ClearReceivedCalls();

            sut.UpdateUserGroupsAndRoles(AccountName, AccountType, userGroups);

            commandBus.Value.Received(1).Send(Arg.Is<Envelope<ICommand>>(o => o.Body is RemoveUserRoleCommand));
            commandBus.Value.Received(1).Send(Arg.Is<Envelope<ICommand>>(o => ((RemoveUserRoleCommand)o.Body).RoleId == role2.Id));
        }

        private void MockUser(CreateExternalUserAccountCommand createExternalUserAccountCommand)
        {
            var user = new User { Id = createExternalUserAccountCommand.UserId };

            queryService.GetUser(Arg.Is<string>(o => o == createExternalUserAccountCommand.Name), 
                (Querying.Data.AccountType)AccountType).Returns(user);
        }

        private void CreateUser()
        {
            sut.AddUser(AccountName, AccountType, DisplayName, Enumerable.Empty<string>());
        }
    }
}