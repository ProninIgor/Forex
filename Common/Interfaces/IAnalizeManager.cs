using Common.Entities;

namespace Common.Interfaces
{
    /// <summary>
    /// Контраст Менеджера анализа
    /// </summary>
    public interface IAnalizeManager
    {
        /// <summary>
        /// Получаем диапазон покупки для маркета с определённым типом
        /// </summary>
        /// <param name="marketId">ИД маркета</param>
        /// <param name="type">алгоритм расчёта секции</param>
        /// <returns></returns>
        StakeSectionBuy Calculate(int marketId, AlgorithmCalculateType type);
    }
}