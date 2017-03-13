using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookkeeping.Models;
using Bookkeeping.Service;
using Bookkeeping.Repositories;

namespace Bookkeeping.Controllers
{
    public class BookkeepingController : Controller
    {
        private readonly BookkeepingService _MoneyBookSvc;
        
        public BookkeepingController()
        {
            var unitOfWork = new EFUnitOfWork();
            _MoneyBookSvc = new BookkeepingService(unitOfWork);
        }

        // GET: Bookkeeping
        public ActionResult Index()
        {            
            return View(_MoneyBookSvc.GetBookkeeping());
        }
        
        [HttpPost]
        public ActionResult Index(int Category)
        {
            return View(_MoneyBookSvc.QueryCategory(Category).OrderByDescending(x=>x.Date));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(_MoneyBookSvc.GetBookkeeping());
        }

        [HttpPost]
        public ActionResult Create(ExpensesRecord data)
        {
            _MoneyBookSvc.AddBookkeeping(data);
            return View(_MoneyBookSvc.GetBookkeeping().OrderByDescending(x=>x.Date));
        }
    }
}