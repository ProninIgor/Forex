using Common.Entities;

namespace Common.Interfaces
{
    /// <summary>
    /// �������� ��������� �������
    /// </summary>
    public interface IAnalizeManager
    {
        /// <summary>
        /// �������� �������� ������� ��� ������� � ����������� �����
        /// </summary>
        /// <param name="marketId">�� �������</param>
        /// <param name="type">�������� ������� ������</param>
        /// <returns></returns>
        StakeSectionBuy Calculate(int marketId, AlgorithmCalculateType type);
    }
}