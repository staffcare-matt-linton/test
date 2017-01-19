using ConsoleApplication.Examples;
using System;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            int f1 = 1;
            int f2 = 0;
            int fn = 0;
            while(fn<100)
            {
                Console.WriteLine(fn);
                f2 = f1;
                f1 = fn;
                fn = f1 + f2;
            }

            //Delegates.Main();
            //Linq.AnonymousTypes();
            //Streams.WriteToFile("");
            //ConnectToDatabase.ConnectionNotCLosed();
            Console.ReadKey();
        }
    }
}
public interface ICollection
{
    int Count { get; }
}

class Class1 : ICollection
{
    public int Count
    {
        get
        {
            return 0;
        }
    }
}