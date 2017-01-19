﻿using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApplication.Solutions
{
    public class Maths
    {
        public static string ToRoman(int number)
        {
            if ((number < 1 || (number > 4999))) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            return string.Empty;
        }

        public static double Factorial(int n)
        {
            if (n < 0)
                throw new ArgumentOutOfRangeException("argument can't be negative");
            double result = 1;
            for (; n>1; n--)
            {
                result *= n;
            }
            return result;
        }

        public static void Fibonacci()
        {
            int f1 = 1, f2 = 1;
            for (int fn = 0; fn < 100; fn = f1 + f2)
            {
                f1 = f2;
                f2 = fn;
                Console.WriteLine(fn);
            }
            Console.ReadKey();
        }

        public static int[] Fibonacci(int limit)
        {
            int[] sequence = new int[limit];

            int f1 = 1, f2 = 1, count = 0;
            for (int fn = 0; count < limit; fn = f1 + f2)
            {
                f1 = f2;
                f2 = fn;
                sequence[count++] = fn;
            }
            return sequence;
        }


    }
}
