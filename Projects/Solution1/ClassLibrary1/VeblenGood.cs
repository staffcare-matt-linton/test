using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class VeblenGood : Product
    {
        public VeblenGood()
        {
        }

        public VeblenGood(string id, string name, double costPrice):base(id, name, costPrice)
        {
            Id = id;
            Name = name;
            CostPrice = costPrice;
        }

        public override double RetailPrice
        {
            get
            {
                return 5 * CostPrice;
            }
        }

    }
}
