using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Common.Entities;
using Common.Interfaces;

namespace Common
{
    /// <summary>
    /// �������� �������
    /// </summary>
    public class AnalizeManager : IAnalizeManager
    {
        /// <summary>
        /// ������ ��� �������
        /// </summary>
        private IObjectManager objectManager { get; set; }

        public AnalizeManager(IObjectManager objectManager)
        {
            this.objectManager = objectManager;
        }

        /// <summary>
        /// �������� �������� ������� ��� ������� � ����������� �����
        /// </summary>
        /// <param name="marketId">�� �������</param>
        /// <param name="type">�������� ������� ������</param>
        /// <returns></returns>
        public StakeSectionBuy Calculate(int marketId, AlgorithmCalculateType type)
        {
            if (type == AlgorithmCalculateType.Agvtype)
            {
                return AgvCalculate(marketId);
            }

            return new StakeSectionBuy(0.00000050, 0.0000055);
        }

        /// <summary>
        /// 10% ������� �� ������������ �������� �� ������
        /// </summary>
        /// <param name="marketId"></param>
        /// <returns></returns>
        private StakeSectionBuy AgvCalculate(int marketId)
        {
            List<TickDTO> ticks = this.objectManager.GetLastTicks(marketId, PeriodType.ThirtyMin, new TimeSpan(-5, 0, 0, 0));

            //
            IEnumerable<double> highValues = ticks.Select(x=>x.HighValue);
            IEnumerable<double> lowValues = ticks.Select(x => x.LowValue);
            double maxValue = highValues.Max();
            double minValue = lowValues.Min();
            double maxRate = minValue + (maxValue - minValue) / 100 * 10;
            return new StakeSectionBuy(minValue, maxRate);
        }
    }
}