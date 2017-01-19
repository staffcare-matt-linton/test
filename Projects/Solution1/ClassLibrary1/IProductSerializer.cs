using System.Collections.Generic;

namespace ClassLibrary1
{
    public interface IProductSerializer
    {
        ICollection<Product> ReadProducts();
        void WriteProducts(ICollection<Product> collection);
    }
}