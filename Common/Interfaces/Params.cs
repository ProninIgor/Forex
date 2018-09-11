using System;
using System.Collections.Generic;
using Common.Entities;

namespace Common.Interfaces
{
    public class Params
    {
        Dictionary<string, string> _dictionary = new Dictionary<string, string>();
        
        public void Add(string name, string value)
        {
            _dictionary[name] = value;
        }
        
        public string GetValueString(string name)
        {
            _dictionary.TryGetValue(name, out var value);
            return value;
        }
        
        public decimal GetValueDecimal(string name)
        {
            if (_dictionary.TryGetValue(name, out var value))
            {
                return decimal.Parse(value);    
            }

            return default(decimal);
        }
        
        public PeriodType GetValuePeriodType(string name)
        {
            if (_dictionary.TryGetValue(name, out var value))
            {
                return (PeriodType)int.Parse(value);    
            }

            return default(PeriodType);
        }

        public TimeSpan? GetValueTimeSpan(string name)
        {
            if (_dictionary.TryGetValue(name, out var value))
            {
                return new TimeSpan(int.Parse(value), 0, 0, 0);    
            }

            return null;
        }
    }
}