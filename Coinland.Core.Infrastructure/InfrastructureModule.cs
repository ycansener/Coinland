using Autofac;
using Coinland.Core.Domain.Interfaces.Services;
using Coinland.Core.Infrastructure.Data.EntityFramework;
using Coinland.Core.Infrastructure.Services;

namespace Coinland.Core.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CoinlandDbContext>().As<CoinlandDbContext>().InstancePerLifetimeScope();
            builder.RegisterType<CurrencyInfoService>().As<ICurrencyInfoService>().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
