using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            return View("DataList", _MoneyBookSvc.QueryCategory(Category));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ExpensesRecord data)
        {
            if (ModelState.IsValid)
            {
                _MoneyBookSvc.AddBookkeeping(data);
            }
            return View("DataList", _MoneyBookSvc.GetBookkeeping().OrderByDescending(x => x.Date));
        }

        public ActionResult DataList()
        {
            return View(_MoneyBookSvc.GetBookkeeping().OrderByDescending(x=>x.Date));
        }
    }
}