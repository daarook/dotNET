using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ProductService : IProductService
    {
        void changeProductStock(Product product, int amount)
        {
            if (product == null)
            {
                throw new ArgumentNullException("product");
            }
            product.Stock = product.Stock + amount;
        }
        Product getProductsInStock()
        {
            return null;
        }
    }
}
