using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


[assembly: InternalsVisibleTo("Users.Test")]
namespace Core.Services.Cache
{
    public class CacheService : ICache
    {


        internal ICache runtimeCache;
        internal ICache diskCache;


		public CacheService()
        {
            runtimeCache = new RuntimeCache();
            diskCache = new DiskCache();
        }


		/// <summary>
        /// Gets a cached Object. First of all, it will try to get from Runtime. 
        /// If it doesn't exists, it will try to get from Disk.
        /// If it's found in Disk, it will add to Runtime.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            T response;

            if (!string.IsNullOrEmpty(key))
            {
                //Due to problems with RunTime Cache with the Roles array, I'll disable...
                //response = runtimeCache.Get<T>(key);

                //if (response == null)
                {
                    response = diskCache.Get<T>(key);

                    //TryAddRuntime(key, response);
                }
            }
			else
            {
                response = default(T);
            }

            return response;
        }

        /// <summary>
        /// List all fileNames in Disk Cache
        /// </summary>
        /// <returns></returns>
        public List<string> ListFileNames()
        {
            return diskCache.ListFileNames();
        }


        private void TryAddRuntime(string key, object value)
        {
            if (value != null)
            {
                runtimeCache.Add(key, value);
            }
        }


        /// <summary>
        /// Add an object with a valid Key into cache (Runtime and Disk)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, object value)
        {
            if (!string.IsNullOrEmpty(key))
            {
                runtimeCache.Add(key, value);
                diskCache.Add(key, value);
            }
        }


		/// <summary>
        /// Removes an object with a valid Key from cache (Runtime and Disk)
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                runtimeCache.Remove(key);
                diskCache.Remove(key);
            }
        }
    }
}
