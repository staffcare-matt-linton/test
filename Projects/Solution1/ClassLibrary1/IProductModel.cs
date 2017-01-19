using System.Collections.Generic;

namespace ClassLibrary1
{
    public interface IProductModel
    {
        ICollection<Product> Products { get; }

        bool Create(Product product);
        bool Delete(string v);
        Product SelectById(string v);
        ICollection<Product> SelectByName(string v);
        bool Update(Product updatedProduct);
    }
}