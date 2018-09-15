using Bittrex.Core;
using Common;
using Common.Interfaces;
using Ninject.Modules;

namespace ConsoleStartService
{
    public class FakeConfigModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAnalizeManager>().To<AnalizeManager>();
            Bind<IMonitoringManager>().To<MonitoringManager>();
            Bind<IObjectManager>().To<ObjectManager>();
            Bind<IOrderManager>().To<OrderManager>();
            Bind<ITradeManager>().To<TradeManager>();
        }
    }
}