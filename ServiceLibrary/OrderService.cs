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
    public class OrderService : IOrderService
    {
        Type providerService = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        public void PlaceOrder(Customer customer, Dictionary<Product,int> orderRows)
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
        public ArrayList GetCustomerOrders(Customer customer)
        {
            using (Model1Container ctx = new Model1Container())
            {

                Order[] ords = ctx.OrderSet.Select(o => o).Where(o => o.Customer == customer).ToArray();
                ArrayList orders = new ArrayList();
                foreach (Order ord in ords)
                {
                    orders.Add(createDTO(ord));
                }
                return orders;
            }
        }
        private OrderDTO createDTO(Order ord)
        {
            OrderDTO order = new OrderDTO();
            order.CustomerID = ord.CustomerId;
            order.OrderDate = ord.OrderDate;
            OrderEntryDTO[] entries = new OrderEntryDTO[ord.OrderEntry.Count];
            int i = 0;
            foreach (OrderEntry ent in ord.OrderEntry)
            {
                OrderEntryDTO entry = new OrderEntryDTO();
                entry.Amount = ent.Amount;
                entry.ProductID = ent.ProductId;
                entries[i] = entry;
                i++;
            }
            order.entries = entries;
            return order;
        }
    }
}
