using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Configuration;
using Bittrex.JsonData;
using Common;
using Common.Data;
using Common.Entities;
using Common.Interfaces;
using Enums;
using Newtonsoft.Json;

namespace Bittrex
{
    /// <summary>
    /// Класс получения обработанных данных с конкретной биржи
    /// </summary>
    public class BitterexObjectManager : IStockExcangeObjectManager
    {
        private IRuntimeMapper mapper;
        

        public BitterexObjectManager()
        {
            MapperConfigurationExpression cfg = new MapperConfigurationExpression();
            cfg.CreateMap<TickJson, TickDTO>();
            cfg.CreateMap<CurrencyJson, CurrencyDTO>()
                .ForMember(d => d.Code, opt => opt.MapFrom(t => t.Currency))
                .ForMember(d => d.Name, opt => opt.MapFrom(t => t.CurrencyLong));
            cfg.CreateMap<MarketJson, MarketDTO>();
            cfg.CreateMap<BuyJson, OrderDTO>()
                .ForMember(d => d.OrderType, opt => opt.UseValue(OrderType.Buy));
            cfg.CreateMap<SellJson, OrderDTO>()
                .ForMember(d => d.OrderType, opt => opt.UseValue(OrderType.Sell));
            cfg.CreateMap<MarketSummaryJson, MarketSummaryDTO>();


            this.mapper = new Mapper(new MapperConfiguration(cfg));
        }

        public List<TickDTO> GetTicks(int marketId, PeriodType periodType)
        {
            BitterexAPI api = new BitterexAPI();
            string bitterexPeriodType = PeriodTypeConvetToString(periodType);
            string marketName = GetMarketName(marketId);
            string ticks = api.GetTicks(marketName, bitterexPeriodType); //"thirtyMin"
            TickRootJson tickRootJson = JsonConvert.DeserializeObject<TickRootJson>(ticks);
            List<TickDTO> result = new List<TickDTO>();
            foreach (TickJson poco in tickRootJson.ItemJsons)
            {
                TickDTO tickDto = this.mapper.Map<TickJson, TickDTO>(poco);
                result.Add(tickDto);
            }

            return result;
        }

        public List<TickDTO> GetLastTicks(int marketId, PeriodType periodType, TimeSpan offset)
        {
            BitterexAPI api = new BitterexAPI();
            string bitterexPeriodType = PeriodTypeConvetToString(periodType);
            string marketName = GetMarketName(marketId);
            string ticks = api.GetTicks(marketName, bitterexPeriodType); //"thirtyMin"
            TickRootJson tickRootJson = JsonConvert.DeserializeObject<TickRootJson>(ticks);
            List<TickDTO> result = new List<TickDTO>();

            DateTime startDateTime = DateTime.Now.Add(offset);

            foreach (TickJson poco in tickRootJson.ItemJsons.Where(x=>x.DateTime > startDateTime))
            {
                TickDTO tickDto = this.mapper.Map<TickJson, TickDTO>(poco);
                result.Add(tickDto);
            }

            return result;
        }

        public string Name {get { return "Bitterix"; }
        }

        private string GetMarketName(int marketId)
        {
            throw new NotImplementedException();
        }

        public List<CurrencyDTO> GetCurrencies()
        {
            BitterexAPI api = new BitterexAPI();
            string currencies = api.GetCurrencies();
            return GetItems<CurrencyDTO, CurrencyRootJson, CurrencyJson>(currencies);
        }

        public List<MarketDTO> GetMarkets()
        {
            BitterexAPI api = new BitterexAPI();
            string markets = api.GetMarkets();
            return GetItems<MarketDTO, MarketRootJson, MarketJson>(markets);
        }

        public List<MarketSummaryDTO> GetMarketSummaries()
        {
            BitterexAPI api = new BitterexAPI();
            string marketSummaries = api.GetMarketSummaries();
            return GetItems<MarketSummaryDTO, MarketSummaryRootJson, MarketSummaryJson>(marketSummaries);
        }

//        public List<OpenOrder> GetOpenOrders(string apiKey, int marketId)
//        {
//            BitterexAPI api = new BitterexAPI();
//            string marketName = GetMarketName(marketId);
//            string openOrders = api.GetOpenOrders(apiKey, marketName);
//            return GetItems<OpenOrder, OpenOrderRootJson, OpenOrderJson>(openOrders);
//        }


        public List<OrderDTO> GetOrders(int marketId)
        {
            BitterexAPI api = new BitterexAPI();
            string marketName = GetMarketName(marketId);
            string orderBooks = api.GetOrderBooks(marketName);
            OrderBookRootJson orderBookRootJson = JsonConvert.DeserializeObject<OrderBookRootJson>(orderBooks);
            List<OrderDTO> result = new List<OrderDTO>();

            if(orderBookRootJson.OrderBookJson.Buys.Count == 0 || orderBookRootJson.OrderBookJson.Sells.Count == 0)
                return new List<OrderDTO>();

            orderBookRootJson.OrderBookJson.Buys.ForEach(x=>result.Add(this.mapper.Map<BuyJson, OrderDTO>(x)));
            orderBookRootJson.OrderBookJson.Sells.ForEach(x=>result.Add(this.mapper.Map<SellJson, OrderDTO>(x)));
            return result;
        }

        public MarketSummaryDTO GetMarketSummary(int marketId)
        {
            BitterexAPI api = new BitterexAPI();
            string marketName = GetMarketName(marketId);
            string marketSummary = api.GetMarketSummary(marketName);
            List<MarketSummaryDTO> marketSummaries = GetItems<MarketSummaryDTO, MarketSummaryRootJson, MarketSummaryJson>(marketSummary);
            
            return marketSummaries[0];
        }

        private List<TResult> GetItems<TResult, TRoot, TItem>(string json)
            where TItem : class 
            where TRoot : IItemJsons<TItem>
        {
            IItemJsons<TItem> itemJsons = JsonConvert.DeserializeObject<TRoot>(json);
            List<TResult> result = new List<TResult>();
            foreach (var x in itemJsons.ItemJsons)
            {
                TResult currency = this.mapper.Map<TItem, TResult>(x);
                result.Add(currency);
            }

            return result;
        }

        /// <summary>
        /// Конвертирует общий тип периода к нужному для конкретной биржи
        /// </summary>
        /// <param name="periodType"></param>
        /// <returns></returns>
        private string PeriodTypeConvetToString(PeriodType periodType)
        {
            switch (periodType)
            {
                    case PeriodType.OneMin:
                        return "oneMin";
                    case PeriodType.ThirtyMin:
                        return "thirtyMin";
            }
            
            throw new NotImplementedException($"Не реализована конвертация periodType для значения {periodType}");
        }
        
    }
}