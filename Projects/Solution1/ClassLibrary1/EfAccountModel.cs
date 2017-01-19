using ClassLibrary1;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace Core.DataAccess.EntityFramework
{
    public class EfAccountModel : ISqlAccountModel
    {
        public ICollection<Account> Accounts
        {
            get
            {
                using (EcommerceContext context = new EcommerceContext())
                {
                    return context.Accounts.ToList();
                }
            }
        }

        public Account this[string id]
        {
            get
            {
                using (EcommerceContext context = new EcommerceContext())
                {
                    return context.Accounts.Find(id);
                }
            }
        }

        public ICollection<Account> SelectByName(string partOfName)
        {
            using (EcommerceContext context = new EcommerceContext())
            {
                return context.Accounts.Where(account => account.Name.Contains(partOfName)).ToList();
            }
        }

        public bool Create(Account account)
        {
            using (EcommerceContext context = new EcommerceContext())
            {
                if (context.Accounts.Find(account.Id) != null)
                    return false;
                context.Entry(account).State = EntityState.Added;
                int rows = context.SaveChanges();
                return rows == 1;
            }
        }

        public bool Update(Account account)
        {
            using (EcommerceContext context = new EcommerceContext())
            {
                if (!context.Accounts.Any(a => a.Id == account.Id))
                    return false;
                context.Entry(account).State = EntityState.Modified;
                int rowsUpdated = context.SaveChanges();
                return rowsUpdated == 1;
            }
        }

        public bool Delete(string id)
        {
            using (EcommerceContext context = new EcommerceContext())
            {
                Account account = context.Accounts.Find(id);
                if (account == null)
                    return false;
                context.Entry(account).State = EntityState.Deleted;
                int rowsDeleted = context.SaveChanges();
                return rowsDeleted == 1;
            }
        }
    }
}