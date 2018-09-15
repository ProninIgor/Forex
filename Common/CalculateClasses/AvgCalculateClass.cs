using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Common.Entities;
using Common.Exceptions;
using Common.Interfaces;

namespace Common.CalculateClasses
{
    public class AvgCalculateClass : ICalculateClass
    {
        /// <summary>
        /// Порог, с которого начинается нижний коридор для ставок (от 1% до 100%)
        /// </summary>
        private decimal delta;
        
        /// <summary>
        /// Тип периода для анализа
        /// </summary>
        private PeriodType periodType;

        /// <summary>
        /// Временной сдвиг, за который анализируем  данные
        /// </summary>
        /// <returns></returns>
        private TimeSpan? timeDiff;

        /// <summary>
        /// Порог, с которого начинается нижний коридор для ставок (от 1% до 100%)
        /// </summary>
        public decimal Delta {
            get
            {
                if(delta == default(decimal))
                    throw new CalculateClassParamException(string.Format(CommonResources.CalculateClassParamExceptionNotInit, nameof(delta)));

                return delta;
            }
        }

        /// <summary>
        /// Тип периода для анализа
        /// </summary>
        public PeriodType PeriodType
        {
            get
            {
                if(periodType == default(PeriodType))
                    throw new CalculateClassParamException(string.Format(CommonResources.CalculateClassParamExceptionNotInit, nameof(periodType)));

                return periodType;
            }
        }

        /// <summary>
        /// Временной сдвиг, за который анализируем  данные
        /// </summary>
        /// <returns></returns>
        public TimeSpan TimeDiff
        {
            get
            {
                if(timeDiff == default(TimeSpan?))
                    throw new CalculateClassParamException(string.Format(CommonResources.CalculateClassParamExceptionNotInit, nameof(timeDiff)));

                return timeDiff.Value;
            }
        }

        /// <summary>
        /// данные для расчёта
        /// </summary>
        private IObjectManager objectManager { get; }
        
        public AvgCalculateClass(IObjectManager objectManager)
        {
            this.objectManager = objectManager;
        }

        /// <summary>
        /// Инициализация параметров
        /// </summary>
        /// <param name="params"></param>
        public void Init(Params @params)
        {
            delta = @params.GetValueDecimal(PredifineParamNames.Delta);
            periodType = @params.GetValuePeriodType(PredifineParamNames.PeriodType);
            timeDiff = @params.GetValueTimeSpan(PredifineParamNames.TimeSpan);
            
        }

        /// <summary>
        /// Валидация переданных параметров, достаточность для выполнения
        /// </summary>
        public void ValidateParam()
        {
            if(delta == default(decimal))
                throw new CalculateClassParamException(string.Format(CommonResources.CalculateClassParamExceptionNotInit, nameof(delta)));
            
            if(periodType == default(PeriodType))
            {
                throw new CalculateClassParamException(string.Format(CommonResources.CalculateClassParamExceptionNotInit, nameof(periodType)));
            }

            if(timeDiff == default(TimeSpan?))
                throw new CalculateClassParamException(string.Format(CommonResources.CalculateClassParamExceptionNotInit, nameof(timeDiff)));
        }

        /// <summary>
        /// Расчет секции ставки
        /// </summary>
        /// <param name="marketId"></param>
        /// <returns></returns>
        public StakeSection Calculate(int marketId)
        {
            return AgvCalculate(marketId);
        }
        
        /// <summary>
        /// 10% коридор из минимального значения за период
        /// </summary>
        /// <param name="marketId"></param>
        /// <returns></returns>
        private StakeSection AgvCalculate(int marketId)
        {
            List<TickDTO> ticks = this.objectManager.GetLastTicks(marketId, PeriodType, TimeDiff);

            //
            IEnumerable<decimal> highValues = ticks.Select(x=>x.HighValue);
            IEnumerable<decimal> lowValues = ticks.Select(x => x.LowValue);
            decimal maxValue = highValues.Max();
            decimal minValue = lowValues.Min();
            decimal maxRate = minValue + (maxValue - minValue) / 100 * Delta;
            return new StakeSection(minValue, maxRate);
        }
    }
}