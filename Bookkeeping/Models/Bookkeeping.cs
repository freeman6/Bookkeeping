using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bookkeeping.Models
{
    
    public partial class ExpensesRecord
    {
        [Display(Name = "序號")]
        public int SerialNo { get; set; }

        [Display(Name = "費用類別")]
        public int Category { get; set; }

        [Display(Name = "費用日期")]
        public DateTime Date { get; set; }

        [Display(Name = "金額")]
        public int Money { get; set; }

        [Display(Name = "說明")]
        public string memo { get; set; }
    }
}