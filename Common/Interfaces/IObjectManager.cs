using System;
using System.Collections.Generic;
using Common.Data;
using Common.Entities;

namespace Common.Interfaces
{
    /// <summary>
    /// Контракт для получение данных для расчётов (обработанные)
    /// </summary>
    public interface IObjectManager
    {
        decimal GetCurrentMarketValue(int marketId);
        decimal GetCurrentMarketBuyValue(int marketId);
        decimal GetCurrentMarketSellValue(int marketId);
        List<TickDTO> GetTicks(int marketId, string periodType);

        /// <summary>
        /// Получить тики за последний период
        /// </summary>
        /// <param name="marketId">ИД магазина</param>
        /// <param name="periodType">Период агрегации (минута, 15 минут, 30 минут)</param>
        /// <param name="offset">Временной промежуток</param>
        /// <returns></returns>
        List<TickDTO> GetLastTicks(int marketId, PeriodType periodType, TimeSpan offset);
    }
}