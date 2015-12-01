using Affecto.IdentityManagement.Store.EntityFramework;
using Autofac;

namespace Affecto.IdentityManagement.Store.Mocking
{
    public class MockPostgreDbRegistration : ModuleRegistration
    {
        protected override void RegisterDbContext(ContainerBuilder builder)
        {
            builder.RegisterType<MockPostgreDbContext>().As<IDbContext>();
        }
    }
}