using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookkeeping.Models;
using Bookkeeping.Service;

namespace Bookkeeping.Controllers
{
    public class BookkeepingController : Controller
    {
        private BookkeepingService MoneyBookSvc = new BookkeepingService();
        
        // GET: Bookkeeping
        public ActionResult Index()
        {
            
            return View(MoneyBookSvc.GetData());
        }

        public ActionResult Create()
        {
            return View(MoneyBookSvc.GetData());
        }

        [HttpPost]
        public ActionResult Create(ExpensesRecord data)
        {
            MoneyBookSvc.AddBookkeeping(data);
            return View(MoneyBookSvc.GetData().OrderByDescending(x=>x.Date));
        }
    }
}