using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class NormalGood : Product
    {
        public override double RetailPrice
        {
            get
            {
                return 2.5 * CostPrice;
            }
        }


    }
}
