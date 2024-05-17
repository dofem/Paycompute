using Microsoft.AspNetCore.Mvc.Rendering;
using PayCompute.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCompute.Services.Interface
{
    public interface IEmployeeService
    {
        Task CreateAsync (Employee employee);
        Employee GetById (int employeeId);
        Task UpdateAsync (Employee employee);
        Task UpdateAsync(int employeeId);
        Task DeleteAsync (int employeeId);
        decimal UnionFees(int id);
        decimal StudentLoanRepaymentAmount(int id, decimal totalAmount);
        IEnumerable<Employee> GetAllEmployees();
        IEnumerable<SelectListItem> GetAllEmployeeForPayroll();
    }
}
