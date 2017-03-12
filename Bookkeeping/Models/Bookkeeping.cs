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
        [DisplayName("序號")]
        public int SerialNo { get; set; }

        [DisplayName("費用類別")]
        public string Category { get; set; }

        [DisplayName("費用日期")]
        public DateTime Date { get; set; }

        [DisplayName("金額")]
        public int Money { get; set; }

        [DisplayName("說明")]
        public string memo { get; set; }
    }
}