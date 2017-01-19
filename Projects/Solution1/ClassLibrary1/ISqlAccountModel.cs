using System.Collections.Generic;

namespace ClassLibrary1
{
    public interface ISqlAccountModel
    {
        Account this[string id] { get; }

        ICollection<Account> Accounts { get; }

        bool Create(Account account);
        bool Delete(string id);
        ICollection<Account> SelectByName(string partOfName);
        bool Update(Account account);
    }
}