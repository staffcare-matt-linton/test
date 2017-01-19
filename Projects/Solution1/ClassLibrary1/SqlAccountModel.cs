using ClassLibrary1.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ClassLibrary1
{
    public class SqlAccountModel : ISqlAccountModel
    {
        private string connectionString = Settings.Default.sqlserver;

        public Account this[string id] { get {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "select * from accounts where id = @id";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("id", id);
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            return
                            new Account
                            {
                                Id = (string)dataReader["id"],
                                Name = (string)dataReader["name"]
                            };
                        }
                    }
                    return null;
                }

            } }

        public ICollection<Account> Accounts {
            get
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "select * from accounts";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    ISet<Account> accounts = new HashSet<Account>();
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            accounts.Add(
                            new Account
                            {
                                Id = (string)dataReader["id"],
                                Name = (string)dataReader["name"]
                            });
                        }
                    }
                    return accounts;
                }
            }
        }

        public ICollection<Account> SelectByName(string partOfName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "select * from accounts where name like @name";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("name", "%" + partOfName + "%");
                ISet<Account> accounts = new HashSet<Account>();
                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        accounts.Add(
                        new Account
                        {
                            Id = (string)dataReader["id"],
                            Name = (string)dataReader["name"]
                        });
                    }
                }
                return accounts;
            }



        }

        public bool Create(Account account)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "insert into accounts values(@id, @name)";// * from accounts where id = @id";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("id", account.Id);
                    cmd.Parameters.AddWithValue("name", account.Name);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool Update(Account account)
        {

            if(account == null) { return false;  }

            string id = account.Id;
            string name = account.Name;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "update accounts set name = @name where id = @id";// * from accounts where id = @id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("name", name);
                return cmd.ExecuteNonQuery() > 0;

            }
        }

        public bool Delete(string id)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "delete from accounts where id = @id";// * from accounts where id = @id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("id", id);
                return cmd.ExecuteNonQuery() > 0;

            }
        }
    }
}