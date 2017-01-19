using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Account
    {
        //instanve variables
        private string id;
        private string name;

        public Account()
        {
        }

        public Account(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public override bool Equals(object obj)
        {
            Account otherAccount = obj as Account;
            return Id == otherAccount.Id;
        }

        //properties
        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        [Required]
        [Column(TypeName ="varchar")]
        [StringLength(250)]
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
    }
}
