using ClassLibrary1;
using ConsoleApplication1.Properties;
using PrimeNumbers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {

        static void Main(string[] args)
        {

            string connectionString = Settings.Default.sqlserver;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "insert into accounts (id, name) values ('acc5','Matt Smith');";
                    int rowsInserted = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }




            Console.ReadKey();
        }
    }


}
