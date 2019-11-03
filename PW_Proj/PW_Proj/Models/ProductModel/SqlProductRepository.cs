using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PW_Proj.Models.ProductModel
{
    public class SqlProductRepository : IProductRepository
    {
        public readonly AppDbContext context;

        public SqlProductRepository(AppDbContext context)
        {
            this.context = context;
        }
        
        public Product AddProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();

            return product;
        }

        public Product Delete(int id)
        {
            Product productToDelete = context.Products.Find(id);

            if (productToDelete != null)
            {
                context.Products.Remove(productToDelete);
                context.SaveChanges();
            }

            return productToDelete;
        }

        public IEnumerable<Product> GetAllProducts()
        {

            return context.Products;
        }

        public Product GetProduct(int id)
        {
            return context.Products.Find(id);
        }

        public Product Update(Product product)
        {
            var productToUpadate = context.Products.Attach(product);
            productToUpadate.State = EntityState.Modified;
            context.SaveChanges();

            return product;
        }
    }
}
