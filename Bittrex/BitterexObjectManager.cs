using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Configuration;
using Bittrex.JsonData;
using Common;
using Common.Data;
using Enums;
using Newtonsoft.Json;

namespace Bittrex
{
    public class BitterexObjectManager : IStockExcangeObjectManager
    {
        private IRuntimeMapper mapper;
        public BitterexObjectManager()
        {
            MapperConfigurationExpression cfg = new MapperConfigurationExpression();
            cfg.CreateMap<TickJson, Tick>();
            cfg.CreateMap<CurrencyJson, Currency>()
                .ForMember(d => d.Code, opt => opt.MapFrom(t => t.Currency))
                .ForMember(d => d.Name, opt => opt.MapFrom(t => t.CurrencyLong));
            cfg.CreateMap<MarketJson, Market>();
            cfg.CreateMap<BuyJson, Order>()
                .ForMember(d => d.OrderType, opt => opt.UseValue(OrderType.Buy));
            cfg.CreateMap<SellJson, Order>()
                .ForMember(d => d.OrderType, opt => opt.UseValue(OrderType.Sell));
            cfg.CreateMap<MarketSummaryJson, MarketSummary>();


            this.mapper = new Mapper(new MapperConfiguration(cfg));
           
            
        }

        public List<Tick> GetTicks(string market, string periodType)
        {
            BitterexAPI api = new BitterexAPI();
            string ticks = api.GetTicks(market, periodType); //"thirtyMin"
            TickRootJson tickRootJson = JsonConvert.DeserializeObject<TickRootJson>(ticks);
            List<Tick> result = new List<Tick>();
            foreach (TickJson poco in tickRootJson.ItemJsons)
            {
                Tick tick = this.mapper.Map<TickJson, Tick>(poco);
                result.Add(tick);
            }

            return result;
        }

        public List<Tick> GetLastTicks(string market, string periodType, TimeSpan offset)
        {
            BitterexAPI api = new BitterexAPI();
            string ticks = api.GetTicks(market, periodType); //"thirtyMin"
            TickRootJson tickRootJson = JsonConvert.DeserializeObject<TickRootJson>(ticks);
            List<Tick> result = new List<Tick>();

            DateTime startDateTime = DateTime.Now.Add(offset);

            foreach (TickJson poco in tickRootJson.ItemJsons.Where(x=>x.DateTime > startDateTime))
            {
                Tick tick = this.mapper.Map<TickJson, Tick>(poco);
                result.Add(tick);
            }

            return result;
        }

        public string Name {
            get { return "Bitterix"; }
        }

        public List<Currency> GetCurrencies()
        {
            BitterexAPI api = new BitterexAPI();
            string currencies = api.GetCurrencies();
            return GetItems<Currency, CurrencyRootJson, CurrencyJson>(currencies);
        }

        public List<Market> GetMarkets()
        {
            BitterexAPI api = new BitterexAPI();
            string markets = api.GetMarkets();
            return GetItems<Market, MarketRootJson, MarketJson>(markets);
        }

        public List<MarketSummary> GetMarketSummaries()
        {
            BitterexAPI api = new BitterexAPI();
            string marketSummaries = api.GetMarketSummaries();
            return GetItems<MarketSummary, MarketSummaryRootJson, MarketSummaryJson>(marketSummaries);
        }

        public List<Order> GetOrders(string marketName)
        {
            BitterexAPI api = new BitterexAPI();
            string orderBooks = api.GetOrderBooks(marketName);
            OrderBookRootJson orderBookRootJson = JsonConvert.DeserializeObject<OrderBookRootJson>(orderBooks);
            List<Order> result = new List<Order>();
            orderBookRootJson.OrderBookJson.Buys.ForEach(x=>result.Add(this.mapper.Map<BuyJson, Order>(x)));
            orderBookRootJson.OrderBookJson.Sells.ForEach(x=>result.Add(this.mapper.Map<SellJson, Order>(x)));
            return result;
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

        
    }
}