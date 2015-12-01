using System;
using Affecto.IdentityManagement.AcceptanceTests.Infrastructure;
using Affecto.IdentityManagement.Interfaces.Model;
using TechTalk.SpecFlow;

namespace Affecto.IdentityManagement.AcceptanceTests.Steps
{
    [Binding]
    internal class GroupSteps : StepDefinition
    {
        [Given(@"the external group name of the group '(.+)' is changed to '(.+)'")]
        [When(@"the external group name of the group '(.+)' is changed to '(.+)'")]
        public void WhenTheExternalGroupNameOfTheGroupIsChangedTo(string groupName, string externalGroup)
        {
            Guid groupId = MockDatabase.GetGroup(groupName).Id;
            IGroup group = IdentityManagementService.GetGroup(groupId);
            Try(() => IdentityManagementService.UpdateGroup(groupId, groupName, group.Description, group.IsDisabled, externalGroup));
        }

        [Given(@"the external group name of the group '(.+)' is cleared")]
        public void GivenTheExternalGroupNameOfTheGroupIsCleared(string groupName)
        {
            WhenTheExternalGroupNameOfTheGroupIsChangedTo(groupName, null);
        }
    }
}