using ClassLibrary1;
using Core.DataAccess.EntityFramework;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using UnitTests.Properties;
using Xunit;

namespace Core.IntegrationTests
{
    //test classes in the same collection don't run in parallel
    [Collection("Collection 1")] 
    public class EfAccountModelTest
    {
        private ISqlAccountModel sut = new EfAccountModel();

        public EfAccountModelTest()
        {
            string connectionString = new Settings().sqlserver;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = File.ReadAllText(@"..\..\setup1.sql");
                cmd.ExecuteNonQuery();
            }
        }

        [Fact]
        [Trait("Core.EF", "Integration Test")]
        public void AccountsProperty_ReturnsAllAccounts()
        {
            //arrange
            //act
            ICollection<Account> Accounts = sut.Accounts;
            //assert
            Assert.NotNull(Accounts);
            Assert.Equal(4, Accounts.Count);
        }

        [Fact]
        [Trait("Core.EF", "Integration Test")]
        public void IndexedProperty_AccountId_ReturnsMatchingAccount()
        {
            //arrange
            //act
            Account account = sut["acc2"];
            //assert
            Assert.NotNull(account);
            Assert.Equal("Jane Jones", account.Name);
        }

        [Fact]
        [Trait("Core.EF", "Integration Test")]
        public void SelectByName_PartOfName_ReturnsMatchingAccounts()
        {
            //arrange
            //act
            ICollection<Account> accounts = sut.SelectByName("sm");
            //assert
            Assert.NotNull(accounts);
            Assert.Equal(2, accounts.Count);
            Assert.NotNull(accounts.FirstOrDefault(a => a.Name == "John Smith"));
            Assert.NotNull(accounts.FirstOrDefault(a => a.Name == "Sue Smedley"));
        }

        [Fact]
        [Trait("Core.EF", "Integration Test")]
        public void Create_Account_ShouldCreateAccount()
        {
            //arrange
            Account account = new Account ("acc5",  "Denis Dawson");
            //act
            bool created = sut.Create(account);
            //assert
            Assert.True(created);
            Assert.Equal(5, sut.Accounts.Count);
        }

        [Fact]
        [Trait("Core.EF", "Integration Test")]
        public void Create_ExistingAccount_ShouldNotCreateAccount()
        {
            //arrange
            Account account = new Account ("acc1",  "John Smith");
            //act
            bool created = sut.Create(account);
            //assert
            Assert.False(created);
            Assert.Equal(4, sut.Accounts.Count);
        }

        [Fact]
        [Trait("Core.EF", "Integration Test")]
        public void Update_Account_ShouldUpdateAccount()
        {
            //arrange
            Account account = new Account ("acc1", "Jim Smith");
            //act
            bool updated = sut.Update(account);
            //assert
            Assert.True(updated);
            Assert.Equal("Jim Smith", sut["acc1"].Name);
        }

        [Fact]
        [Trait("Core.EF", "Integration Test")]
        public void Update_AccountNotInDataStore_ShouldNotAddAccount()
        {
            //arrange
            Account account = new Account ("acc5", "Jan Smuts");
            //act
            bool updated = sut.Update(account);
            //assert
            Assert.False(updated);
            Assert.Equal(4, sut.Accounts.Count);
        }

        [Fact]
        [Trait("Core.EF", "Integration Test")]
        public void Delete_Account_ShouldRemoveAccount()
        {
            //arrange
            //act
            bool deleted = sut.Delete("acc1");
            //assert
            Assert.True(deleted);
            Assert.Null(sut["acc1"]);
            Assert.Equal(3, sut.Accounts.Count);
        }

        [Fact]
        [Trait("Core.EF", "Integration Test")]
        public void Delete_AccountNotInDataStore_ShouldReturnFalse()
        {
            //arrange
            //act
            bool deleted = sut.Delete("acc5");
            //assert
            Assert.False(deleted);
        }
    }
}
