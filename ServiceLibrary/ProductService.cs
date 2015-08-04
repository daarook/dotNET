using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Collections;

namespace ServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ProductService : IProductService
    {
        Type providerService = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        public void ChangeProductStock(Product product, int amount)
        {
            if (product == null)
            {
                throw new ArgumentNullException("product");
            }
            product.Stock += amount;

        }
        public ProductDTO[] GetProductsInStock()
        {
            using (Model1Container ctx = new Model1Container())
            {
                Product[] prods = ctx.ProductSet.Select(p => p).Where(p => p.Stock > 0).ToArray();
                ArrayList products = new ArrayList();
                foreach(Product prod in prods) {
                    products.Add(createDTO(prod));
                }
                Object[] temp = products.ToArray();
                ProductDTO[] result = Array.ConvertAll(temp, x => (ProductDTO)x);
                return result;
            }
        }
        public void addProduct(string name, double price, int stock)
        {
            using (Model1Container ctx = new Model1Container())
            {
                if (!ctx.ProductSet.Any(c => c.Name == name))
                {
                    Product p = new Product { Name = name, Price = price, Stock = stock};
                    ctx.ProductSet.Add(p);
                    ctx.SaveChanges();
                }
                else
                {
                    throw new FaultException("product already exists");
                }
            }
        }
        private ProductDTO createDTO(Product prod)
        {
            ProductDTO product = new ProductDTO();
            product.Name = prod.Name;
            product.Price = prod.Price;
            product.Stock = prod.Stock;
            return product;
        }
    }
}
