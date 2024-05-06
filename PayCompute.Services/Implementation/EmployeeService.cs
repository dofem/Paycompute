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
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        public EmployeeService(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task CreateAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public  Employee GetById(int employeeId)
        {
            return _context.Employees.Where(u=> u.Id == employeeId).FirstOrDefault();
            
        }

        public async Task DeleteAsync(int employeeId)
        {
            var employee = GetById(employeeId);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }

       

        public decimal StudentLoanRepaymentAmount(int id, decimal totalAmount)
        {
            throw new NotImplementedException();
        }

        public decimal UnionFees(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int employeeId)
        {
            var employee = GetById(employeeId) as Employee;
            _context.Employees.Update(employee);
            _context.SaveChangesAsync();
        }
    }
}
