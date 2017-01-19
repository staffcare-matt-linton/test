using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Product
    {
        //overloaded constructures
        public Product(string id, string name, double costPrice, double retailPrice = 0)
        {
            Id = id;
            Name = name;
            CostPrice = costPrice;
            RetailPrice = retailPrice;
        }

        public Product()
        {
        }

        public override bool Equals(object obj)
        {
            Product otherProduct = obj as Product;
            return Id == otherProduct.Id; 
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        // auto implemented properties
        [ScaffoldColumn(true)]
        [Required(ErrorMessage = "Id is Required")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        public double CostPrice { get; set; }
        public virtual double RetailPrice { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }


    }
}
