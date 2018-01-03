using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bittrex.JsonData;
using Common.Data;
using Newtonsoft.Json;

namespace Bittrex
{
    public class BitterexObjectManager
    {
        static BitterexObjectManager()
        {
            Mapper.Initialize(cfg=>cfg.CreateMap<TickPoco, Tick>());
        }

        public List<Tick> GetTicks(string market, string periodType)
        {
            BitterexAPI api = new BitterexAPI();
            string ticks = api.GetTicks(market, periodType); //"thirtyMin"
            TickRootPoco tickRootPoco = JsonConvert.DeserializeObject<TickRootPoco>(ticks);
            List<Tick> result = new List<Tick>();
            foreach (TickPoco poco in tickRootPoco.TickPocos)
            {
                Tick tick = Mapper.Map<TickPoco, Tick>(poco);
                result.Add(tick);
            }

            return result;
        }

        public List<Tick> GetLastTicks(string market, string periodType, TimeSpan offset)
        {
            BitterexAPI api = new BitterexAPI();
            string ticks = api.GetTicks(market, periodType); //"thirtyMin"
            TickRootPoco tickRootPoco = JsonConvert.DeserializeObject<TickRootPoco>(ticks);
            List<Tick> result = new List<Tick>();

            DateTime startDateTime = DateTime.Now.Add(offset);

            foreach (TickPoco poco in tickRootPoco.TickPocos.Where(x=>x.DateTime > startDateTime))
            {
                Tick tick = Mapper.Map<TickPoco, Tick>(poco);
                result.Add(tick);
            }

            return result;
        }
    }
}