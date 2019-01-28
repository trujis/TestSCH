using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

[assembly: InternalsVisibleTo("Users.Test")]
namespace Core.Services.Cache
{
    internal class RuntimeCache : ICache
    {

        public T Get<T>(string key)
        {
            T response;

            if (!string.IsNullOrEmpty(key))
            {
                var item = HttpRuntime.Cache.Get(key);

                if (item != null)
                {
                    response = (T)item;
                }
                else
                {
                    response = default(T);
                }
            }
            else
            {
                response = default(T);
            }

            return response;
        }


        public void Add(string key, object value)
        {
            HttpRuntime.Cache.Insert(key, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration);
        }


        public void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }


        public List<string> ListFileNames()
        {
            return new List<string>();
        }
    }
}
