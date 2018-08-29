using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Common.Entities;

namespace Common
{
    public class AnalizeManager : IAnalizeManager
    {
        private IObjectManager objectManager { get; set; }

        public AnalizeManager(IObjectManager objectManager)
        {
            this.objectManager = objectManager;
        }

        public AnalizeSection Calculate(int marketId, CalculateType type)
        {
            if (type == CalculateType.Agvtype)
            {
                return AgvCalculate(marketId);
            }

            return new AnalizeSection(){MinRate = 0.00000050, MaxRate = 0.0000055};
        }

        /// <summary>
        /// 10% коридор из минимального значения за период
        /// </summary>
        /// <param name="marketId"></param>
        /// <returns></returns>
        private AnalizeSection AgvCalculate(int marketId)
        {
            List<Tick> ticks = this.objectManager.GetLastTicks(marketId, PeriodType.ThirtyMin, new TimeSpan(-5, 0, 0, 0));

            //
            IEnumerable<double> highValues = ticks.Select(x=>x.HighValue);
            IEnumerable<double> lowValues = ticks.Select(x => x.LowValue);
            double maxValue = highValues.Max();
            double minValue = lowValues.Min();
            double maxRate = minValue + (maxValue - minValue) / 100 * 10;
            return new AnalizeSection(minValue, maxRate);
        }
    }
}