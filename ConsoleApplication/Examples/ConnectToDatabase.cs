using ConsoleApplication.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.Examples
{
    public class ConnectToDatabase
    {
        public static void ConnectionNotCLosed()
        {
            string connectionString = Settings.Default.sqlserver;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "insert into accounts (id, name) values ('acc1','John Smith');";
            int rowsInserted = cmd.ExecuteNonQuery();
            connection.Close();
        }

        public static void FinallyBlock()
        {
            string connectionString = Settings.Default.sqlserver;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "insert into account (id, name) values ('acc1','John Smith');";
                int rowsInserted = cmd.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public static void UsingBlock()
        {
            string connectionString = Settings.Default.sqlserver;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    try
                    {
                        cmd.CommandText = "insert into account (id, name) values ('acc1','John Smith');";

                        int rowsInserted = cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        //the first expression will be executed
        public static void WithoutTransactions()
        {
            string connectionString = Settings.Default.sqlserver;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    try
                    {
                        //valid expression
                        cmd.CommandText = "insert into accounts (id, name) values ('acc1','John Smith');";
                        int rowsInserted = cmd.ExecuteNonQuery();
                        //invalid expression
                        cmd.CommandText = "update account set name = 'Jane Smith' where id = 'acc1';";
                        int rowsUpdated = cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        //the first expression will be executed
        public static void WithTransactions()
        {
            string connectionString = Settings.Default.sqlserver;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.Serializable);
                    cmd.Connection = connection;                    
                    cmd.Transaction = transaction;
                    try
                    {
                        //valid expression
                        cmd.CommandText = "insert into accounts (id, name) values ('acc1','John Smith');";
                        int rowsInserted = cmd.ExecuteNonQuery();
                        //invalid expression
                        cmd.CommandText = "update account set name = 'Jane Smith' where id = 'acc1';";
                        int rowsUpdated = cmd.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}



//cmd.CommandText = "update account set name = 'Jane Smith' where id = 'acc1';";
//int rowsUpdated = cmd.ExecuteNonQuery();