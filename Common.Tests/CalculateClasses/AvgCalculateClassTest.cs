using System;
using System.Collections.Generic;
using Common.CalculateClasses;
using Common.Data;
using Common.Entities;
using Common.Exceptions;
using Common.Interfaces;
using Common.Tests.Managers;
using Moq.Language.Flow;
using Xunit;
using NAssert = NUnit.Framework.Assert;


namespace Common.Tests.CalculateClasses
{
    public class AvgCalculateClassTest
    {
        [Fact]
        public void AvgCalculateClassConstructorTest()
        {
            MockObjectManager mockObjectManager = new MockObjectManager();
            ITestResult<IObjectManager, StakeSectionBuy> testResult = mockObjectManager.GetTestResult();
            AvgCalculateClass avgCalculateClass = new AvgCalculateClass(testResult.Data);
            Assert.NotNull(avgCalculateClass);
        }

        [Fact]
        public void AvgCalculateClass_InitTest()
        {
            MockObjectManager mockObjectManager = new MockObjectManager();
            ITestResult<IObjectManager, StakeSectionBuy> testResult = mockObjectManager.GetTestResult();
            AvgCalculateClass avgCalculateClass = new AvgCalculateClass(testResult.Data);

            Params @params = new Params();
            @params.Add(PredifineParamNames.Delta, 10.ToString());
            @params.Add(PredifineParamNames.PeriodType, ((int) PeriodType.ThirtyMin).ToString());
            @params.Add(PredifineParamNames.TimeSpan, 0.ToString());
            avgCalculateClass.Init(@params);

            NAssert.DoesNotThrow(() => avgCalculateClass.ValidateParam());
        }
        
        [Fact]
        public void AvgCalculateClass_InitTest1()
        {
            MockObjectManager mockObjectManager = new MockObjectManager();
            ITestResult<IObjectManager, StakeSectionBuy> testResult = mockObjectManager.GetTestResult();
            AvgCalculateClass avgCalculateClass = new AvgCalculateClass(testResult.Data);

            Params @params = new Params();
            @params.Add(PredifineParamNames.Delta, 10.ToString());
            @params.Add(PredifineParamNames.TimeSpan, 0.ToString());
            avgCalculateClass.Init(@params);

            NAssert.Throws<CalculateClassParamException>(() => avgCalculateClass.ValidateParam());
        }
        
        [Fact]
        public void AvgCalculateClass_InitTest2()
        {
            MockObjectManager mockObjectManager = new MockObjectManager();
            ITestResult<IObjectManager, StakeSectionBuy> testResult = mockObjectManager.GetTestResult();
            AvgCalculateClass avgCalculateClass = new AvgCalculateClass(testResult.Data);

            Params @params = new Params();
            avgCalculateClass.Init(@params);

            NAssert.Throws<CalculateClassParamException>(() => avgCalculateClass.ValidateParam());
        }
        

        [Fact]
        public void AvgCalculateClass_CalculateTest()
        {

            MockObjectManager mockObjectManager = new MockObjectManager();
            ITestResult<IObjectManager, StakeSectionBuy> testResult =
                mockObjectManager.GetTestResult(1, PeriodType.ThirtyMin, TimeSpan.Zero);
            IObjectManager objectManager = testResult.Data;
            StakeSectionBuy expectedResult = testResult.Result;

            AvgCalculateClass avgCalculateClass = new AvgCalculateClass(objectManager);
            Params @params = new Params();
            @params.Add(PredifineParamNames.Delta, 10.ToString());
            @params.Add(PredifineParamNames.PeriodType, ((int) PeriodType.ThirtyMin).ToString());
            @params.Add(PredifineParamNames.TimeSpan, 0.ToString());
            avgCalculateClass.Init(@params);
            StakeSection actual = avgCalculateClass.Calculate(1);

            Assert.Equal(expectedResult.MinRate, actual.MinRate);
            Assert.Equal(expectedResult.MaxRate, actual.MaxRate);
        }

        [Fact]
        public void AvgCalculateClass_CalculateTest1()
        {
            MockObjectManager mockObjectManager = new MockObjectManager();
            ITestResult<IObjectManager, StakeSectionBuy> testResult =
                mockObjectManager.GetTestResult1(1, PeriodType.ThirtyMin);
            IObjectManager objectManager = testResult.Data;
            StakeSectionBuy expectedResult = testResult.Result;

            AvgCalculateClass avgCalculateClass = new AvgCalculateClass(objectManager);
            Params @params = new Params();
            @params.Add(PredifineParamNames.Delta, 5.ToString());
            @params.Add(PredifineParamNames.PeriodType, ((int) PeriodType.ThirtyMin).ToString());
            @params.Add(PredifineParamNames.TimeSpan, 0.ToString());
            avgCalculateClass.Init(@params);
            StakeSection actual = avgCalculateClass.Calculate(1);

            Assert.Equal(expectedResult.MinRate, actual.MinRate);
            Assert.Equal(expectedResult.MaxRate, actual.MaxRate);
        }
    }
}