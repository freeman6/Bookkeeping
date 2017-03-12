using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bookkeeping.Models
{
    [Table("AccountBook")]
    public partial class ExpensesRecord
    {
        [Key]
        [Column("Id")]
        [DisplayName("序號")]
        public Guid SerialNo { get; set; }

        [Column("Categoryyy", TypeName = "int")]
        [DisplayName("費用類別")]
        public int Category { get; set; }

        [Column("Dateee")]
        [DisplayName("費用日期")]
        public DateTime Date { get; set; }

        [Column("Amounttt")]
        [DisplayName("金額")]
        public int Money { get; set; }

        [Column("Remarkkk")]
        [MaxLength(500,ErrorMessage = "欄位資料長度不得大於500")]
        [DisplayName("說明")]
        public string memo { get; set; }
    }
}