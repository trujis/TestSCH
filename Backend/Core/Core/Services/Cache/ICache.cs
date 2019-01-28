using System;
using System.Collections.Generic;

namespace Core.Services.Cache
{
    public interface ICache
    {

        T Get<T>(string key);

        List<string> ListFileNames();

        void Add(string key, object value);

        void Remove(string key);

    }
}