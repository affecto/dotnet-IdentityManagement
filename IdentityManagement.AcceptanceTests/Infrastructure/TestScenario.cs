using System;
using System.Linq;
using Affecto.AuditTrail.Interfaces;
using Affecto.Authentication.Claims;
using Affecto.IdentityManagement.AcceptanceTests.Mocking;
using Affecto.IdentityManagement.Store.EntityFramework;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TechTalk.SpecFlow;
using ModuleRegistration = Affecto.IdentityManagement.Autofac.ModuleRegistration;

namespace Affecto.IdentityManagement.AcceptanceTests.Infrastructure
{
    [Binding]
    internal static class TestScenario
    {
        [BeforeScenario]
        public static void Setup()
        {
            var builder = new ContainerBuilder();
            RegisterProductionCodeModules(builder);

            SetupMockDatabase(builder);
            SetupMockAuthenticatedUserContext(builder);
            SetupSubstitutes(builder);

            SetupContainer(builder);
        }

        [AfterScenario]
        public static void TearDown()
        {
            Assert.IsFalse(ScenarioContext.Current.Any(pair => pair.Value is Exception), "Unhandled exception was thrown in scenario.");
        }

        private static void SetupContainer(ContainerBuilder builder)
        {
            IContainer container = builder.Build();
            ScenarioContext.Current.Set(container);
        }

        private static void RegisterProductionCodeModules(ContainerBuilder builder)
        {
            builder.RegisterModule<ModuleRegistration>();
            builder.RegisterModule<Store.EntityFramework.ModuleRegistration>();
        }

        private static void SetupMockAuthenticatedUserContext(ContainerBuilder builder)
        {
            var authenticatedUserContext = Substitute.For<IAuthenticatedUserContext>();
            builder.RegisterInstance(authenticatedUserContext).As<IAuthenticatedUserContext>();
            var mockAuthenticatedUserContext = new MockAuthenticatedUserContext(authenticatedUserContext);
            ScenarioContext.Current.Set(mockAuthenticatedUserContext);
        }

        private static void SetupMockDatabase(ContainerBuilder builder)
        {
            var dbContext = new MockDbContext();
            builder.RegisterInstance(dbContext).As<IDbContext>();
            var database = new MockDatabase(dbContext);
            ScenarioContext.Current.Set(database);
        }

        private static void SetupSubstitutes(ContainerBuilder builder)
        {
            var auditTrailService = Substitute.For<IAuditTrailService>();
            builder.RegisterInstance(auditTrailService);
        }
    }
}