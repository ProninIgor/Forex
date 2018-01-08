using System.Collections.Generic;

namespace Bittrex.JsonData
{
    public interface IItemJsons<T>
        where T : class 
    {
        List<T> ItemJsons { get; set; }
    }
}