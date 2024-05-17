using Microsoft.AspNetCore.Mvc.Rendering;
using PayCompute.Entity;
using PayCompute.Persistence.Data;
using PayCompute.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCompute.Services.Implementation
{
    public class PayComputationService : IPayComputationService
    {
        private decimal contractualEarnings;
        private decimal overtimeHours;
        private readonly ApplicationDbContext _context;

        public PayComputationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public decimal ContractualEarnings(decimal contractualHours, decimal hoursWorked, decimal hourlyRate)
        {
            if(hoursWorked < contractualHours)
            {
                contractualEarnings  = hoursWorked * hourlyRate;
            }
            else
            {
                contractualEarnings = contractualHours * hourlyRate;
            }
            return contractualEarnings;
        }

        public async Task CreateAsync(PaymentRecord paymentRecord)
        {
            await _context.PaymentRecords.AddAsync(paymentRecord);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<PaymentRecord> GetAll()
        {
            return _context.PaymentRecords.OrderBy(p => p.EmployeeId).ToList();
        }

        public IEnumerable<SelectListItem> GetAllTaxYear()
        {
            var allTaxYear = _context.TaxYears.Select(taxYears => new SelectListItem
            {
                Text = taxYears.YearOfTax,
                Value = taxYears.Id.ToString()
            });   
                return allTaxYear;
        }

        public PaymentRecord GetById(int id)
        {
            return _context.PaymentRecords.Where(p => p.Id == id).FirstOrDefault();    
        }

        public decimal NetPay(decimal totalEarnings, decimal totalDeduction) => (totalEarnings - totalDeduction);
        public decimal OvertimeEarnings(decimal overtimeRate, decimal overtimeHours) => (overtimeRate * overtimeHours);
        public decimal OverTimeHours(decimal hoursWorked, decimal contractualHours)
        {
            if(hoursWorked <= contractualHours)
            {
                overtimeHours = 0.00m;
            }
            else
            {
                overtimeHours = hoursWorked-contractualHours;
            }
            return overtimeHours;
        }

        public decimal OvertimeRate(decimal hourlyRate) => hourlyRate * 1.5m;
        
        public decimal TotalDeduction(decimal tax, decimal nic, decimal studentLoanRepayment, decimal unionFees)
        {
            return tax + nic + studentLoanRepayment + unionFees;
        }

        public decimal TotalEarnings(decimal overtimeEarnings, decimal contractualEarnings)
        {
            return overtimeEarnings + contractualEarnings;
        }

        public TaxYear GetTaxYearById(int id)=> _context.TaxYears.Where(y =>y.Id == id).FirstOrDefault();   
        
    }
}
