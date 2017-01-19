using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Maths
    {
        public static int Factorial(int v)
        {

            if (v < 0)
                throw new ArgumentOutOfRangeException("Value cannot be negative");

            int result = 1;

            for (int i = v; v > 1; v--)
            {
                //result = result * v;
                result *= v;
            }
            return result;
        }

        internal static double Combination(int n, int r)
        {
            double value = 0;
            double fn_n = Maths.Factorial(n);
            double fn_r = Maths.Factorial(r);
            double fn_nr = Maths.Factorial(n - r);

            value = fn_n / (fn_r * fn_nr);

            return value;
        }

        public static string ToRoman(int number)
        {
            if (number >= 40) return string.Empty;
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            return string.Empty;
        }
    }
}
