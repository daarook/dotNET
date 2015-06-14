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
            using (Model1Container ctx = new Model1Container())
            {
                Order order = new Order { OrderDate = DateTime.Now, Customer = customer };
                ctx.OrderSet.Add(order);
                foreach (KeyValuePair<Product, int> row in orderRows)
                {
                    Product product = row.Key;
                    int amount = row.Value;
                    OrderEntry orderEntry = new OrderEntry { Product = product, Amount = amount, Order = order };
                    ctx.OrderEntrySet.Add(orderEntry);
                }
                ctx.SaveChanges();
            }
        }
        public Order[] getCustomerOrders(Customer customer)
        {
            using (Model1Container ctx = new Model1Container())
            {
                return ctx.OrderSet.Select(o => o).Where(o => o.Customer == customer).ToArray();
            }
        }

    }
}
