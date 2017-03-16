using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;
using Bookkeeping.Repositories;

namespace Bookkeeping.Services
{

    public class CacheServices<T>
    {
        static int intExpireMinute = 30;
        static string _CacheName { get; set; }

        public CacheServices(string CacheName)
        {
            _CacheName = CacheName;
        }

        /// <summary>
        /// 將資料寫入Cache
        /// </summary>
        /// <param name="source"></param>
        /// <param name="cacheName"></param>
        public void AddCache(IEnumerable<T> source)
        {
            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy();

            policy.AbsoluteExpiration = DateTime.Now.AddMinutes(intExpireMinute);
            cache.Add(_CacheName, source, policy);
        }

        /// <summary>
        /// 從Cache讀出資料
        /// </summary>
        /// <param name="cacheName"></param>
        /// <returns></returns>
        public IEnumerable<T> GetCache()
        {
            ObjectCache cache = MemoryCache.Default;
            CacheItem cacheContents = cache.GetCacheItem(_CacheName);

            if (cacheContents == null)
            {
                return null;
            }
            else
            {
                return cacheContents.Value as IEnumerable<T>;
            }

            #region returnCache
            //if (TargetData == null)
            //{

            //    return TargetData;
            //}
            //else
            //{
            //    return cacheContents.Value as List<T>;
            //}
            #endregion

        }
    }
}