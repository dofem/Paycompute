using PayCompute.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCompute.Services.Implementation
{
    public class TaxService : ITaxService
    {
        public decimal TaxAmount(decimal totalAmount)
        {
            decimal _taxRate = 0;
            decimal _tax = 0;
            if(totalAmount <= 1042) 
            {
                //Tax Free Rate
                _taxRate = .0m;
                _tax  =  totalAmount * _taxRate;
            }
            else if(totalAmount > 1042 && totalAmount <= 3125)
            {
                //Basic tax rate
                _taxRate = .20m;
                //income tax
                _tax = (1042 * .0m) +((totalAmount - 1042) * _taxRate);
            }
            else if (totalAmount > 3125 && totalAmount <= 12500)
            {
                //Higher tax rate
                _taxRate = .40m;
                //Income tax
                _tax = (1042 * .0m) + ((3125 - 1042) * .20m) + ((totalAmount - 3125) * _taxRate);
            }
            else if(totalAmount > 12500)
            {
                //Additional tax  Rate
                _taxRate = .45m;
                //Income tax
                _tax = (1042 * .0m) + ((3125 - 1042) * .20m) + ((12500 - 3125) * 0.40m) + ((totalAmount - 12500) * _taxRate);
            }
            return _tax;
        }
    }
}
