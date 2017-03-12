using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookkeeping.Models;
using System.Runtime.Caching;
using Bookkeeping.Services;
using System.Linq.Expressions;

namespace Bookkeeping.Service
{
   public class BookkeepingService
    {
        static int intExpireMinute = 30;
        static string cacheName = "MoneyBook";
        static List<ExpensesRecord> MoneyBook;

        /// <summary>
        /// 取得初始值的記帳本記錄
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ExpensesRecord> GetBookkeeping()
        {
            MoneyBook = new List<ExpensesRecord> {
                new ExpensesRecord{ SerialNo=1, Category="收入", Date = new DateTime(2016,1,1), Money=600, memo=""},
                new ExpensesRecord{ SerialNo=2, Category="支出", Date = new DateTime(2016,1,2), Money=1200, memo=""},
                new ExpensesRecord{ SerialNo=3, Category="支出", Date = new DateTime(2016,1,3), Money=860, memo=""},
                new ExpensesRecord{ SerialNo=4, Category="收入", Date = new DateTime(2016,1,4), Money=1800, memo=""},
            };

            AddCache(MoneyBook, cacheName);
            
            return GetData();
        }
        /// <summary>
        /// 新增記帳本記錄
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public IEnumerable<ExpensesRecord> AddBookkeeping(ExpensesRecord record)
        {
            record.SerialNo = MoneyBook.Count()+1;
            MoneyBook.Add(record);
            AddCache(MoneyBook, cacheName);

            return GetData();
        }

        /// <summary>
        /// 將記帳本資料寫入Cache
        /// </summary>
        /// <param name="source"></param>
        /// <param name="cacheName"></param>
        private static void AddCache(IEnumerable<ExpensesRecord> source, string cacheName)
        {
            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy();

            policy.AbsoluteExpiration = DateTime.Now.AddMinutes(intExpireMinute);
            cache.Add(cacheName, source, policy);
        }

        /// <summary>
        /// 從Cache讀出記帳本資料
        /// </summary>
        /// <param name="cacheName"></param>
        /// <returns></returns>
        public IEnumerable<ExpensesRecord> GetData()
        {
            ObjectCache cache = MemoryCache.Default;
            CacheItem cacheContents = cache.GetCacheItem(cacheName);

            if (cacheContents == null)
            {
                return GetBookkeeping();
            }
            else
            {
                return cacheContents.Value as IEnumerable<ExpensesRecord>;
            }
        }
    }
}