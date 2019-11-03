using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PW_Proj.Models.ProductModel
{
    public interface IProductRepository
    {
        Product GetProduct(int id);
        IEnumerable<Product> GetAllProducts();
        Product AddProduct(Product product);
        Product Update(Product product);
        Product Delete(int id);
    }
}
