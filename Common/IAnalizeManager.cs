using Common.Entities;

namespace Common
{
    public interface IAnalizeManager
    {
        AnalizeSection Calculate(int marketId);
    }

    public class AnalizeManager : IAnalizeManager
    {
        public AnalizeSection Calculate(int marketId)
        {
            return new AnalizeSection(){MinRate = 0, MaxRate = 1};
        }
    }
}