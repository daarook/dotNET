using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class OrderService : IOrderService
    {
        public void placeOrder(Customer customer, Dictionary<Product,int> orderRows)
        {
            Order order = new Order{OrderDate = }
            foreach (KeyValuePair<Product, int> row in orderRows)
            {
                Product product = row.Key;
                int amount = row.Value;

            }
        }
    }
}
