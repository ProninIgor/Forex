using Common.Entities;

namespace Common
{
    public interface IAnalizeManager
    {
        AnalizeSection Calculate(int marketId, CalculateType type);
    }
}