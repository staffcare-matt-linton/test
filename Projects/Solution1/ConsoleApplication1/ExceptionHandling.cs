using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class ExceptionHandling
    {
        static void Main(string[] args)
        {
            try
            {
                WriteToFile();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }

        private static void WriteToFile()
        {
            File.WriteAllText(@"z:\file.txt", "Something");
        }
    }
}
