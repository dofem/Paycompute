using PayCompute.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCompute.Services.Implementation
{
    public class NationalInsuranceContributionService : INationalInsuranceContributionService
    {
        private decimal NIRate;

        public decimal NIC;

        public decimal NIContribution(decimal totalAmount)
        {
            if(totalAmount < 719)
            {
                //Lower earning Limit Rate & below primary threshold
                NIRate = .0m;
                NIC = .0m;

            }
            else if (totalAmount >= 719 && totalAmount <= 4167)
            {
                //Between Primary Threshold and Upper erarning limit
                NIRate = .12m;
                NIC = ((totalAmount - 719) * NIRate);
            }
            else if(totalAmount > 4167)
            {
                //Above upper earning limit
                NIRate = .02m;
                NIC = ((4167 - 719) * .12m) + ((totalAmount - 4167) * NIRate);
            }
            return NIC;
        }
    }
}
