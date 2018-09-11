using Common.Entities;

namespace Common.Interfaces
{
    /// <summary>
    /// Контракт расчёта
    /// </summary>
    public interface ICalculateClass
    {
        /// <summary>
        /// Инициализация параметров
        /// </summary>
        /// <param name="params"></param>
        void Init(Params @params);
        
        /// <summary>
        /// Валидация переданных параметров, достаточность для выполнения
        /// </summary>
        void ValidateParam();
        
        /// <summary>
        /// Расчет секции ставки
        /// </summary>
        /// <param name="marketId"></param>
        /// <returns></returns>
        StakeSectionBuy Calculate(int marketId);
    }
}