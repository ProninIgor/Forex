using System;
using Enums;

namespace Common.Entities
{

    /// <summary>
    /// �������� �� ������ 
    /// </summary>
    public class OrderCandidate
    {
        public Guid Id { get; set; }

        /// <summary>
        /// �������, ��� �������� ���� ������� ������
        /// </summary>
        public int MarketId { get; set; }

        /// <summary>
        /// ��� ������ (Sell, Buy)
        /// </summary>
        public OrderType OrderType { get; set; }

        /// <summary>
        /// ������ ������
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// ���� ������ � �������� ������
        /// </summary>
        public decimal Rate { get; set; }
    }

}