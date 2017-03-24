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
            return View("DataList", _MoneyBookSvc.QueryCategory(Category));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAjax(ExpensesRecord data)
        {
            if (ModelState.IsValid)
            {
                var result = new AjaxReturnViewModel() { IsSuccess=true,Goto= Url.Action("DataList") };
                return Json(result);
            }
            return PartialView("_CreateForm",data);

        }

        [HttpPost]
        public ActionResult Create(ExpensesRecord data)
        {

            string errMessage = "";

            if (data.Money < 0)
            {
                errMessage = "．「金額」資料僅接受正整數<br>";
            }
            if (data.Date > DateTime.Now.Date)
            {
                errMessage = errMessage + $"．「日期」資料不能超過今日{DateTime.Now.ToShortDateString()}<br>";
            }
            if (data.memo.Length > 100)
            {
                errMessage = errMessage + "．「備註」資料僅接受100字元";
            }

            if (errMessage.Length == 0)
            {
                _MoneyBookSvc.AddBookkeeping(data);
                return View("DataList", _MoneyBookSvc.GetBookkeeping().OrderByDescending(x => x.Date));
            }
            else
            {
                return Content(errMessage);
            }
        }

        public ActionResult DataList()
        {
            return View(_MoneyBookSvc.GetBookkeeping().OrderByDescending(x=>x.Date));
        }

    }
}