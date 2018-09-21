using System;
using Bittrex;
using Bittrex.Core;
using Common;
using Common.Interfaces;
using Common.IoC;
using ConsoleStartService.FakeImplimintations;

namespace ConsoleStartService
{
    class Program
    {
        static void Main(string[] args)
        {
            IoC.SetFake(new FakeConfigModule());
            IStockExcangeObjectManager fakeObjectManager = new FakeObjectManager();
            IObjectManager objectManager = new ObjectManager(fakeObjectManager);
            MonitoringManager monitoringManager = new MonitoringManager(new[]{2}, fakeObjectManager, objectManager);
            monitoringManager.Init();
            monitoringManager.Start();
        }
    }
}