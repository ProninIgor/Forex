﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Common;
using Common.Data;
using DAL;

namespace RecipientData
{
    public class Copier
    {
        private IStockExcangeObjectManager objectManager;

        private IRuntimeMapper mapper;


        public Copier(IStockExcangeObjectManager objectManager)
        {
            this.objectManager = objectManager;

            MapperConfigurationExpression cfg = new MapperConfigurationExpression();
            cfg.CreateMap<Currency, CurrencyPoco>();
            cfg.CreateMap<Market, MarketPoco>();
            cfg.CreateMap<MarketSummary, MarketSummaryPoco>();
            cfg.CreateMap<Order, OrderPoco>().
                ForMember(d => d.BaseVolume, opt => opt.ResolveUsing(t => t.Rate * t.Quantity));

            this.mapper = new Mapper(new MapperConfiguration(cfg));
        }

        public void StartCurrencies()
        {
            List<Currency> currencies = this.objectManager.GetCurrencies();
            List<CurrencyPoco> currencyPocos = this.mapper.Map<List<Currency>, List<CurrencyPoco>>(currencies);
            using (ConnectionDb connection = new ConnectionDb())
            {
                IEnumerable<CurrencyPoco> pocoDb = connection.GetAll<CurrencyPoco>();
                Dictionary<string, CurrencyPoco> pocosDictionary = pocoDb.ToDictionary(x => x.Code);



                foreach (CurrencyPoco currencyPoco in currencyPocos)
                {
                    CurrencyPoco value;
                    if (pocosDictionary.TryGetValue(currencyPoco.Code, out value))
                    {
                        currencyPoco.Id = value.Id;
                    }
                }


                connection.Insert(currencyPocos.Where(x => x.Id == 0).ToList());
                connection.Update(currencyPocos.Where(x => x.Id > 0).ToList());
            }
        }

        public void StartMarkets()
        {
            List<Market> markets = this.objectManager.GetMarkets();
            using (ConnectionDb connection = new ConnectionDb())
            {



                IEnumerable<CurrencyPoco> currencies = connection.GetAll<CurrencyPoco>();
                Dictionary<string, int> currenciesDictionary = currencies.ToDictionary(x => x.Code, y => y.Id);

                int currencyId;
                foreach (Market marketPoco in markets)
                {
                    if (!currenciesDictionary.TryGetValue(marketPoco.BaseCurrency, out currencyId))
                        throw new ArgumentException("Please update Currencies");

                    marketPoco.BaseCurrencyId = currencyId;

                    if (!currenciesDictionary.TryGetValue(marketPoco.MarketCurrency, out currencyId))
                        throw new ArgumentException("Please update Currencies");

                    marketPoco.MarketCurrencyId = currencyId;
                }

                List<MarketPoco> marketPocos = this.mapper.Map<List<Market>, List<MarketPoco>>(markets);

                IEnumerable<MarketPoco> pocoDb = connection.GetAll<MarketPoco>();
                Dictionary<string, MarketPoco> pocosDictionary = pocoDb.ToDictionary(x => x.MarketName);



                foreach (MarketPoco marketPoco in marketPocos)
                {
                    MarketPoco value;
                    if (pocosDictionary.TryGetValue(marketPoco.MarketName, out value))
                    {
                        marketPoco.Id = value.Id;
                    }
                }


                connection.Insert(marketPocos.Where(x => x.Id == 0).ToList());
                connection.Update(marketPocos.Where(x => x.Id > 0).ToList());
            }
        }

        public void StartMarketSummaries()
        {
            using (ConnectionDb connection = new ConnectionDb())
            {
                IEnumerable<MarketPoco> marketPocos = connection.GetAll<MarketPoco>();
                Dictionary<string, int> marketDictionary = marketPocos.ToDictionary(x => x.MarketName, y => y.Id);

                List<MarketSummary> marketSummaries = this.objectManager.GetMarketSummaries();

                int marketId;
                foreach (MarketSummary marketSummary in marketSummaries)
                {
                    if (!marketDictionary.TryGetValue(marketSummary.MarketName, out marketId))
                        throw new ArgumentException("Please update Markets");

                    marketSummary.MarketId = marketId;

                }

                List<MarketSummaryPoco> marketSummaryPocos =
                    this.mapper.Map<List<MarketSummary>, List<MarketSummaryPoco>>(marketSummaries);
                connection.Insert(marketSummaryPocos);
                
            }
        }

        public void StartOrders()
        {
            DateTime timeStamp = DateTime.Now;
            using (ConnectionDb connection = new ConnectionDb())
            {
                List<MarketPoco> marketPocos = connection.GetAll<MarketPoco>().Where(x=>x.IsActive && x.BaseCurrencyId == 2).ToList();
                Dictionary<string, int> marketDictionary = marketPocos.ToDictionary(x => x.MarketName, y => y.Id);

                ConcurrentDictionary<string, List<Order>> orderDictionary = new ConcurrentDictionary<string, List<Order>>();

                Parallel.ForEach(marketPocos, x => { orderDictionary[x.MarketName] = GetOrders(x.MarketName); });

                Parallel.ForEach(orderDictionary, pair =>
                {
                    List<OrderPoco> orderPocos = this.mapper.Map<List<Order>, List<OrderPoco>>(pair.Value);
                    orderPocos.ForEach(x =>
                    {
                        x.MarketId = marketDictionary[pair.Key];
                        x.TimeStamp = timeStamp;
                    });

                    using (ConnectionDb insertConnection = new ConnectionDb())
                    {
                        orderPocos.ForEach(insertConnection.Insert);
                    }
                    
                });

               
            }
        }

        private List<Order> GetOrders(string marketName)
        {
            return this.objectManager.GetOrders(marketName);
        }

        private string GetRootDirectory()
        {
            return "d:\\ExcangeData";
        }
    }
}
