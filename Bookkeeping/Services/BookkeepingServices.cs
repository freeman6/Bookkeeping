using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookkeeping.Models;
using System.Runtime.Caching;
using Bookkeeping.Services;
using Bookkeeping.Repositories;
using System.Linq.Expressions;

namespace Bookkeeping.Service
{
   public class BookkeepingService
    {
        static string cacheName = "MoneyBook";
        CacheServices<ExpensesRecord> MoneyBookCache = new CacheServices<ExpensesRecord>(cacheName);

        private readonly IRepository<AccountBook> _AccountBook;
        private readonly IUnitOfWork _unitOfWork;
        
        public BookkeepingService(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
            _AccountBook = new Repository<AccountBook>(UnitOfWork);
        }

        /// <summary>
        /// 取得初始值的記帳本記錄
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ExpensesRecord> GetBookkeeping()
        {
            if (MoneyBookCache.GetCache() == null)
            {
                var source = _AccountBook.GetALL().Select(x => new ExpensesRecord
                {
                    Category = x.Categoryyy,
                    Date = x.Dateee,
                    Money = x.Amounttt,
                    memo = x.Remarkkk
                });

                MoneyBookCache.AddCache(source);
                return source;
            }
            else
            {
                var source = MoneyBookCache.GetCache();
                return source;
            }
        }

        /// <summary>
        /// 新增記帳本記錄
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public void AddBookkeeping(ExpensesRecord record)
        {
            //物件資料移轉
            AccountBook targetRecord = new AccountBook();
            targetRecord.Id = Guid.NewGuid();
            targetRecord.Categoryyy = record.Category;
            targetRecord.Dateee = record.Date == null ? DateTime.Now.Date: record.Date;
            targetRecord.Amounttt = record.Money;
            targetRecord.Remarkkk = record.memo == null ? "": record.memo;

            _AccountBook.Create(targetRecord);
            _AccountBook.Commit();

            //MoneyBookCache.GetCache().ToList().Add(record);
            MoneyBookCache.AddCache(record);
        }

        public IEnumerable<ExpensesRecord> QueryCategory(int tmpCategory)
        {
            var source = _AccountBook.GetALL().Select(x => new ExpensesRecord
            {
                Category = x.Categoryyy,
                Date = x.Dateee,
                Money = x.Amounttt,
                memo = x.Remarkkk
            });

            return source.Where(x => x.Category == tmpCategory);
        }
    }
}