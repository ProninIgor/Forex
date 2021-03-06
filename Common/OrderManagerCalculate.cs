﻿using System;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;
using Common.CalculateClasses;
using Common.Entities;
using Common.Exceptions;
using Common.Interfaces;
using Enums;

namespace Common
{
    /// <summary>
    /// Менеджер ставок. Хранит в себе инфу по ставкам, принимает решения о том, как расчитывать ставки
    /// </summary>
    public partial class OrderManager
    {
        
        private decimal GetCurrentRate(int marketId)
        {
            return this.ObjectManager.GetCurrentMarketValue(marketId);
        }

        /// <summary>
        /// Привязаться к анализу оредеров на покупку, чтобы загнать цену как можно ниже
        /// </summary>
        /// <param name="marketId"></param>
        /// <returns></returns>
        private decimal GetBuyRate(int marketId)
        {

            return GetCurrentRate(marketId);
        }

        private decimal GetQuantity(int marketId)
        {
            //todo определение размера ставки исходя из минимального значения, успешности метода вычисления и вероятности попадания. подтверждения со стороны пользователя
            return 0.0005m;
        }

        private OrderCandidate GetBuyOrderCandidate(int marketId)
        {
            ICalculateClass avgCalculateClass = new AvgCalculateClass(this.ObjectManager);
            
            Params @params = new Params();
            @params.Add(PredifineParamNames.Delta, 10.ToString());
            @params.Add(PredifineParamNames.PeriodType, 30.ToString());
            @params.Add(PredifineParamNames.TimeSpan, 1.ToString());
            avgCalculateClass.Init(@params);
            
            
            StakeSection stakeSectionBuy = avgCalculateClass.Calculate(marketId);
            if (stakeSectionBuy == null)
                return null;

            var geyOrderCandidate = GetOrderCandidate(marketId, stakeSectionBuy);
            return geyOrderCandidate;
        }

        private OrderCandidate GetOrderCandidate(int marketId, StakeSection stakeSectionBuy)
        {
            decimal currentRate = GetCurrentRate(marketId);

            //todo проверки на маркетИд
            if (stakeSectionBuy.InSection(currentRate))
            {
                decimal buyRate = GetBuyRate(marketId);
                //todo проверку на вхождение в диапазон

                decimal quantity = GetQuantity(marketId);
                return new OrderCandidate()
                {
                    Id = Guid.NewGuid(),
                    MarketId = marketId,
                    OrderType = OrderType.Buy,
                    Rate = currentRate,
                    Quantity = quantity
                };
            }

            return null;
        }
        
        private OrderCandidate GetOrderCandidateSell(int marketId, StakeSection stakeSectionBuy)
        {
            decimal currentRate = GetCurrentRate(marketId);

            //todo проверки на маркетИд
            if (stakeSectionBuy.InSectionSell(currentRate))
            {
                decimal buyRate = GetBuyRate(marketId);
                //todo проверку на вхождение в диапазон

                decimal quantity = GetQuantity(marketId);
                return new OrderCandidate()
                {
                    Id = Guid.NewGuid(),
                    MarketId = marketId,
                    OrderType = OrderType.Buy,
                    Rate = currentRate,
                    Quantity = quantity
                };
            }

            return null;
        }

        private StakeSection GetStakeSectionSell(int marketId)
        {
            OrderBO orderBo = this._orderBos[marketId];
            var minOrder = orderBo.Rate = orderBo.Rate + Math.Abs(orderBo.Rate * 0.01m);
            var maxOrder = minOrder + Math.Abs(minOrder * 0.5m);
            return new StakeSection(minOrder, maxOrder);
        }

        /// <summary>
        /// Запускает работу модуля
        /// </summary>
        public void Working()
        {
            while (true)
            {
                Thread.Sleep(1*100);
                foreach (int marketId in marketIds)
                {
                    OrderBOStatus orderBoStatus = this._orderBos[marketId].Status;

                    switch (orderBoStatus)
                    {
                        case OrderBOStatus.Start:
                            StakeSection stakeSectionBuy = GetStakeSectionBuy(marketId);
                            if (stakeSectionBuy != null)
                            {
                                this._orderBos[marketId].StakeSectionBuy = stakeSectionBuy;
                                this._orderBos[marketId].Status = OrderBOStatus.FindSectionBuy;  
                            }
                            break;
                        case OrderBOStatus.FindSectionBuy:
                            OrderCandidate orderCandidateBuy = GetOrderCandidate(marketId, this._orderBos[marketId].StakeSectionBuy);
                            if (orderCandidateBuy != null)
                            {
                                this._orderBos[marketId].OrderCandidateBuy = orderCandidateBuy;
                                this._orderBos[marketId].Status = OrderBOStatus.FindOrderBuy;
                            }
                            break;
                        case OrderBOStatus.FindOrderBuy:
                        case OrderBOStatus.SetOrderBuy:
                           break;
                        
//                            StakeSection stakeSectionSell = GetStakeSectionSell(this._orderBos[marketId]);
//                            if (stakeSectionSell != null)
//                            {
//                                this._orderBos[marketId].StakeSectionSell = stakeSectionSell;
//                                this._orderBos[marketId].Status = OrderBOStatus.FindSectionSell; 
//                            }
//                            break;

                      
                        case OrderBOStatus.CompliteBuy:
                            StakeSection stakeSectionSell = GetStakeSectionSell(marketId);
                            if (stakeSectionSell != null)
                            {
                                this._orderBos[marketId].StakeSectionSell = stakeSectionSell;
                                this._orderBos[marketId].Status = OrderBOStatus.FindSectionSell;  
                            }
                            break;
//                            var candidate = GetOrderCandidate(marketId, stakeSection);
//
//                            this._orderBos[marketId].StakeSectionSell = stakeSection;
//                            this._orderBos[marketId].OrderCandidateSell = candidate;
//                            this._orderBos[marketId].Status = OrderBOStatus.FindOrderSell;
                           
                        case OrderBOStatus.FindSectionSell:
                            OrderCandidate orderCandidateSell = GetOrderCandidateSell(marketId, this._orderBos[marketId].StakeSectionSell);
                            if (orderCandidateSell != null)
                            {
                                this._orderBos[marketId].OrderCandidateSell = orderCandidateSell;
                                this._orderBos[marketId].Status = OrderBOStatus.FindOrderSell;
                            }
                            break;
                        case OrderBOStatus.FindOrderSell:
                        case OrderBOStatus.SetOrderSell:
                            break;

                        case OrderBOStatus.CompliteSell:
                            //todo save data;
                            this._orderBos[marketId].Status = OrderBOStatus.Start;
                            break;

                        default:
                            throw new OrderManagerException();
                    }
//                    if(_marketOrderStatuses[marketId].IsHaveOneBuyOrder)
//                        continue;
                    
                    
                    
//                    if (HasBuyOrder(marketId))
//                    {
//                        continue;
//                    }


                    

                    

                    //this.orderCandidates.TryUpdate(GetBuyKey(marketId), orderCandidate, null);
                }
            }
        }

        private StakeSection GetStakeSectionBuy(int marketId)
        {
            ICalculateClass avgCalculateClass = new AvgCalculateClass(this.ObjectManager);
            
            Params @params = new Params();
            @params.Add(PredifineParamNames.Delta, 10.ToString());
            @params.Add(PredifineParamNames.PeriodType, 30.ToString());
            @params.Add(PredifineParamNames.TimeSpan, 1.ToString());
            avgCalculateClass.Init(@params);
            
            
            StakeSection stakeSectionBuy = avgCalculateClass.Calculate(marketId);
            if (stakeSectionBuy == null)
                return null;

            return stakeSectionBuy;
//            var minOrder = orderBo.Rate = orderBo.Rate + orderBo.Rate * 0.01m;
//            var maxOrder = minOrder + minOrder * 0.1m;
//            return new StakeSection(minOrder, maxOrder);
        }
    }
}