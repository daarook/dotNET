﻿using System;
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
        public void changeProductStock(Product product, int amount)
        {
            if (product == null)
            {
                throw new ArgumentNullException("product");
            }
            product.Stock += amount;

        }
        public Product[] getProductsInStock()
        {
            using (Model1Container ctx = new Model1Container())
            {
                return ctx.ProductSet.Select(p => p).Where(p => p.Stock > 0).ToArray();
            }
        }
    }
}
