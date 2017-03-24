using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bookkeeping.Models
{
    
    public partial class ExpensesRecord
    {
        [Display(Name = "序號")]
        public int SerialNo { get; set; }

        [Display(Name = "費用類別")]
        [Required(ErrorMessage = "必要欄位")]
        public int Category { get; set; }

        [Display(Name = "費用日期")]
        [Required(ErrorMessage = "必要欄位")]
        [DateAttribute(ErrorMessage = "ERROR")]
        public DateTime Date { get; set; }

        [Display(Name = "金額")]
        [Required(ErrorMessage = "必要欄位")]
        [Range(100, int.MaxValue, ErrorMessage = "金額必須為正整數")]
        public int Money { get; set; }

        [Display(Name = "說明")]
        [Required(ErrorMessage ="必要欄位")]
        [StringLength(100, ErrorMessage = "說明欄位資料不得超過100字元")]
        public string memo { get; set; }
    }

    sealed class DateAttribute : ValidationAttribute
    {
        static DateTime dtStart;
        static DateTime dtEnd;

        public DateAttribute()
        {
            dtStart = DateTime.Now.AddYears(-5);
            dtEnd = DateTime.Now.Date;
        }
        // Methods
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dtDate = (DateTime)value;
            

            if (dtDate <= DateTime.Now.Date)
            {
                // valid
                return ValidationResult.Success;
            }
            else
            {
                // invalid
                var errorMsg = $"輸入的日期({dtDate.ToShortDateString()})不得超過今日{DateTime.Now.ToShortDateString()}";
                return new ValidationResult(errorMsg);
            }
        }
    }
}