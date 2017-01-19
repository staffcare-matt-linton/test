using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ClassLibrary1.Properties;


namespace ClassLibrary1
{
    public class EcommerceContext : DbContext
    {

        public EcommerceContext() : base(Settings.Default.sqlserver)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }


    }
}
