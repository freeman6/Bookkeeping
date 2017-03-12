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

        private readonly IRepository<ExpensesRecord> _AccountBook;
        private readonly IUnitOfWork _unitOfWork;
        
        public BookkeepingService(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
            _AccountBook = new Repository<ExpensesRecord>(UnitOfWork);
        }

        /// <summary>
        /// 取得初始值的記帳本記錄
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ExpensesRecord> GetBookkeeping()
        {
            return _AccountBook.GetALL();

            //暫時用不到了
            //var MoneyBook = _AccountBook.GetALL();
            //MoneyBookCache.AddCache(MoneyBook);
            //return MoneyBookCache.GetCache();
        }
        /// <summary>
        /// 新增記帳本記錄
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public void AddBookkeeping(ExpensesRecord record)
        {
            record.SerialNo = Guid.NewGuid();
            _AccountBook.Create(record);
            _AccountBook.Commit();


            //暫時用不到了
            //MoneyBookCache.AddCache(GetBookkeeping());
            //return MoneyBookCache.GetCache().OrderByDescending(x=>x.Date);
        }

        public IEnumerable<ExpensesRecord> QueryCategory(int tmpCategory)
        {
            return _AccountBook.Query(x => x.Category == tmpCategory);
        }
    }
}