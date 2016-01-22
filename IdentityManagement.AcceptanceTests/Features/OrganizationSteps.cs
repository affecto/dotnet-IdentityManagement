using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.Authentication.Claims;
using Affecto.IdentityManagement.AcceptanceTests.Infrastructure;
using Affecto.IdentityManagement.Interfaces.Exceptions;
using Affecto.IdentityManagement.Interfaces.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Affecto.IdentityManagement.AcceptanceTests.Features
{
    [Binding]
    [Scope(Feature = "Organizations")]
    internal class OrganizationSteps : StepDefinition
    {
        private List<IOrganization> organizations;

        [Given(@"an organization '(.+)' is added with a description '(.+)'")]
        [When(@"an organization '(.+)' is added with a description '(.+)'")]
        public void WhenAnOrganizationIsAddedWithADescrption(string name, string description)
        {
            IOrganization organization = IdentityManagementService.CreateOrganization(name, description, string.Empty);
            nameIdentifierPairs.Add(name, organization.Id);
        }

        [Given(@"an organization '(.+)' is added with an email '(.+)'")]
        [When(@"an organization '(.+)' is added with an email '(.+)'")]
        public void WhenAnOrganizationIsAddedWithAnEmail(string name, string email)
        {
            IOrganization organization = IdentityManagementService.CreateOrganization(name, string.Empty, email);
            nameIdentifierPairs.Add(name, organization.Id);
        }

        [Given(@"an organization '(.+)' is added")]
        public void GivenAnOrganizationIsAdded(string name)
        {
            WhenAnOrganizationIsAddedWithADescrption(name, string.Empty);
        }

        [When(@"an organization '(.+)' is added")]
        public void WhenAnOrganizationIsAdded(string name)
        {
            Try(() => GivenAnOrganizationIsAdded(name));
        }

        [When(@"the name of the organization '(.+)' is changed to '(.+)'")]
        public void WhenTheNameOfTheOrganizationIsChangedTo(string oldName, string newName)
        {
            Guid organizationId = nameIdentifierPairs[oldName];
            Try(() => IdentityManagementService.UpdateOrganization(organizationId, newName, string.Empty, string.Empty, false));
        }

        [When(@"the description of the organization '(.+)' is cleared")]
        [When(@"the email of the organization '(.*)' is cleared")]
        public void WhenTheDescriptionOfTheOrganizationIsCleared(string name)
        {
            Guid organizationId = nameIdentifierPairs[name];
            Try(() => IdentityManagementService.UpdateOrganization(organizationId, name, string.Empty, string.Empty, false));
        }

        [When(@"the organization '(.+)' is disabled")]
        public void WhenTheOrganizationIsDisabled(string name)
        {
            Guid organizationId = nameIdentifierPairs[name];
            Try(() => IdentityManagementService.UpdateOrganization(organizationId, name, string.Empty, string.Empty, true));
        }

        [When(@"an organization with no name is added")]
        public void WhenAnOrganizationWithNoNameIsAdded()
        {
            Try(() => IdentityManagementService.CreateOrganization(string.Empty, string.Empty, string.Empty));
        }

        [When(@"the name of the organization '(.+)' is cleared")]
        public void WhenTheNameOfTheOrganizationIsCleared(string organization)
        {
            Guid organizationId = nameIdentifierPairs[organization];
            Try(() => IdentityManagementService.UpdateOrganization(organizationId, string.Empty, string.Empty, string.Empty, false));
        }

        [Then(@"the following organizations exist:")]
        public void ThenTheFollowingOrganizationsExist(Table expectedOrganizations)
        {
            organizations = IdentityManagementService.GetOrganizations().ToList();
            Assert.AreEqual(expectedOrganizations.RowCount, organizations.Count);
            foreach (TableRow expectedOrganization in expectedOrganizations.Rows)
            {
                Assert.IsTrue(organizations.Any(o => o.Name.Equals(expectedOrganization["Name"])
                    && o.Description.Equals(expectedOrganization["Description"])
                    && o.Email.Equals(expectedOrganization["Email"])),
                    expectedOrganization.ToTableString());
            }
        }

        [Then(@"all organizations are enabled")]
        public void ThenAllOrganizationsAreEnabled()
        {
            foreach (IOrganization organization in organizations)
            {
                Assert.IsFalse(organization.IsDisabled, organization.Name);
            }
        }

        [Then(@"the organization '(.+)' is disabled")]
        public void ThenTheOrganizationIsDisabled(string name)
        {
            Guid organizationId = nameIdentifierPairs[name];
            Assert.IsTrue(IdentityManagementService.GetOrganization(organizationId).IsDisabled);
        }

        [Then(@"adding the organization fails")]
        [Then(@"updating the organization fails")]
        public void ThenAddingTheOrganizationFails()
        {
            AssertCaughtException<ArgumentException>();
        }

        [Then(@"updating the organization fails because of invalid permissions")]
        [Then(@"adding the organization fails because of invalid permissions")]
        public void ThenUpdatingTheOrganizationFailsBecauseOfInvalidPermissions()
        {
            AssertCaughtException<InsufficientPermissionsException>();
        }

        [Then(@"operation fails because organization with the same name already exists")]
        public void ThenOperationFailsBecauseOrganizationWithTheSameNameAlreadyExists()
        {
            AssertCaughtException<OrganizationWithSameNameAlreadyExistsException>();
        }
    }
}