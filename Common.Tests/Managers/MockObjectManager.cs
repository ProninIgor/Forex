using System;
using System.Collections.Generic;
using Common.Data;
using Common.Entities;
using Common.Interfaces;
using Moq;
using Moq.Language.Flow;

namespace Common.Tests.Managers
{
    public class MockObjectManager
    {
        internal ITestResult<IObjectManager, StakeSectionBuy> GetTestResult()
        {
            TestResult<IObjectManager, StakeSectionBuy> testResult = new TestResult<IObjectManager, StakeSectionBuy>();

            using (var mock = Autofac.Extras.Moq.AutoMock.GetLoose())
            {
                mock.Mock<IObjectManager>();
                testResult.Data = mock.Create<IObjectManager>();
            }
            
            testResult.Result = new StakeSectionBuy(0.000m, 0000m);

            return testResult;
        }

        internal ITestResult<IObjectManager, StakeSectionBuy> GetTestResult(int marketId, PeriodType periodType, TimeSpan offset)
        {
            TestResult<IObjectManager, StakeSectionBuy> testResult = new TestResult<IObjectManager, StakeSectionBuy>();

            using (var mock = Autofac.Extras.Moq.AutoMock.GetLoose())
            {
                mock.Mock<IObjectManager>()
                    .Setup(x => x.GetLastTicks(marketId, periodType, offset))
                    .Returns(new List<TickDTO>()
                                {
                                        new TickDTO() {HighValue = 0.00095m, LowValue = 0.00092m},
                                        new TickDTO() {HighValue = 0.00100m, LowValue = 0.00095m},
                                        new TickDTO() {HighValue = 0.00092m, LowValue = 0.00092m},
                                        new TickDTO() {HighValue = 0.00092m, LowValue = 0.00090m},
                                        new TickDTO() {HighValue = 0.00092m, LowValue = 0.00097m},
                                        new TickDTO() {HighValue = 0.00097m, LowValue = 0.00092m}
                                }
                );

                testResult.Data = mock.Create<IObjectManager>();
            }
            
            testResult.Result = new StakeSectionBuy(0.0009m, 0.00091m);

            return testResult;

        }
        
        internal ITestResult<IObjectManager, StakeSectionBuy> GetTestResult1(int marketId, PeriodType periodType)
        {
            TestResult<IObjectManager, StakeSectionBuy> testResult = new TestResult<IObjectManager, StakeSectionBuy>();

            using (var mock = Autofac.Extras.Moq.AutoMock.GetLoose())
            {
                mock.Mock<IObjectManager>()
                    .Setup(x => x.GetLastTicks(marketId, periodType, TimeSpan.Zero))
                    .Returns(new List<TickDTO>()
                        {
                            new TickDTO() {LowValue = 0.0000100m, HighValue = 0.0000120m},
                            new TickDTO() {LowValue = 0.0000120m, HighValue = 0.0000140m},
                            new TickDTO() {LowValue = 0.0000140m, HighValue = 0.0000150m},
                            new TickDTO() {LowValue = 0.0000150m, HighValue = 0.0000170m},
                            new TickDTO() {LowValue = 0.0000170m, HighValue = 0.0000190m},
                            new TickDTO() {LowValue = 0.0000190m, HighValue = 0.0000200m}
                        }
                    );

                testResult.Data = mock.Create<IObjectManager>();
            }
            
            testResult.Result = new StakeSectionBuy(0.0000100m, 0.0000105m);

            return testResult;

        }
    }
}