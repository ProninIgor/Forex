using Common.Entities;

namespace Common
{
    public interface IAnalizeManager
    {
        StakeSectionBuy Calculate(int marketId, AlgorithmCalculateType type);
    }
}