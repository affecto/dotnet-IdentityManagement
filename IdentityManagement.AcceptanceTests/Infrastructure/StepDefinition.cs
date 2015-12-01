using System;
using System.Collections.Generic;
using Affecto.IdentityManagement.Interfaces;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Affecto.IdentityManagement.AcceptanceTests.Infrastructure
{
    internal abstract class StepDefinition
    {
        private const string IdentityManagementServiceException = "IdentityManagementServiceException";

        protected readonly IDictionary<string, Guid> nameIdentifierPairs;

        protected StepDefinition()
        {
            nameIdentifierPairs = new Dictionary<string, Guid>();
        }

        protected static IIdentityManagementService IdentityManagementService
        {
            get
            {
                var container = Get<IContainer>();
                return container.Resolve<IIdentityManagementService>();
            }
        }

        protected static IUserService UserService
        {
            get
            {
                var container = Get<IContainer>();
                return container.Resolve<IUserService>();
            }
        }

        protected static MockDatabase MockDatabase
        {
            get { return Get<MockDatabase>(); }
        }

        protected static MockAuthenticatedUserContext MockAuthenticatedUserContext
        {
            get { return Get<MockAuthenticatedUserContext>(); }
        }

        protected static void Try(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                ScenarioContext.Current.Add(IdentityManagementServiceException, e);
            }
        }

        protected static void AssertCaughtException<TException>() where TException : Exception
        {
            Assert.IsTrue(ScenarioContext.Current.Get<Exception>(IdentityManagementServiceException) is TException);
            ScenarioContext.Current.Remove(IdentityManagementServiceException);
        }

        private static T Get<T>()
        {
            return ScenarioContext.Current.Get<T>();
        }
    }
}