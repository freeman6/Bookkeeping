using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookkeeping.Models
{
    public class AjaxReturnViewModel
    {
        public bool IsSuccess { get; set; }
        public string Goto { get; set; }
    }
}