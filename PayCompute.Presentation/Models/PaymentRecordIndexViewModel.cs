﻿using PayCompute.Entity;
using System.ComponentModel.DataAnnotations;

namespace PayCompute.Models
{
    public class PaymentRecordIndexViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [Display(Name ="Name")]
        public string FullName { get; set; }
        [Display(Name = "Pay Date")]
        public DateTime PayDate { get; set; }
        [Display(Name = "Month")]
        public string PayMonth { get; set; }
        public int TaxYearId { get; set; }
        public string Year { get; set; }
        [Display(Name = "Total Earning")]
        public decimal TotalEarnings { get; set; }
        [Display(Name = "TotalDeductions")]
        public decimal TotalDeduction { get; set;}
        [Display(Name = "Net")]
        public decimal NetPayment { get; set;}
    }
}
