using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceLibrary
{
    public class StoreService : IStoreService
    {
        Type providerService = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        public string Register(string name)
        {
            using(Model1Container ctx = new Model1Container()) {
                if (!ctx.CustomerSet.Any(c => c.Name == name))
                {
                    char[] charArray = name.ToCharArray();
                    Array.Reverse(charArray);
                    string password = new string(charArray);
                    Customer customer = new Customer { Name = name, Password = password, Saldo=50.0};
                    ctx.CustomerSet.Add(customer);
                    ctx.SaveChanges();
                    return password;
                }
                else
                {
                    throw new FaultException("customer already exists");
                }             
            }
        }
        public CustomerDTO Authenticate(string username, string password)
        {
            using (Model1Container ctx = new Model1Container())
            {
                if(ctx.CustomerSet.Any(c => c.Name == username)){
                    Customer customer = ctx.CustomerSet.Single(c => string.Equals(c.Name, username));
                    if (string.Equals(password, customer.Password))
                    {
                        return createDTO(customer);
                    }          
                }
                throw new FaultException("password is incorrect or user does not exist");
            }
        }
        private CustomerDTO createDTO(Customer customer)
        {
            CustomerDTO cus = new CustomerDTO();
            cus.Name = customer.Name;
            cus.Password = customer.Password;
            cus.Saldo = customer.Saldo;
            return cus;
        }

        public ProductDTO[] GetProductsInStock()
        {
            using (Model1Container ctx = new Model1Container())
            {
                Product[] prods = ctx.ProductSet.Select(p => p).Where(p => p.Stock > 0).ToArray();
                ArrayList products = new ArrayList();
                foreach (Product prod in prods)
                {
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
                    Product p = new Product { Name = name, Price = price, Stock = stock };
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

        public void PlaceOrder(string customerName, Dictionary<string, int> orderRows)
        {
            using (Model1Container ctx = new Model1Container())
            {
                Customer customer = ctx.CustomerSet.First(c => c.Name == customerName);
                Order order = new Order { OrderDate = DateTime.Now, Customer = customer };
                ctx.OrderSet.Add(order);
                foreach (KeyValuePair<string, int> row in orderRows)
                {
                    Product product = ctx.ProductSet.First(c => c.Name == row.Key);
                    int amount = row.Value;
                    OrderEntry orderEntry = new OrderEntry { Product = product, Amount = amount, Order = order };
                    ctx.OrderEntrySet.Add(orderEntry);
                    customer.Saldo += -product.Price * amount;
                    if (customer.Saldo < 0)
                    {
                        throw new FaultException("Not enough balance remaining");
                    }
                    product.Stock += -amount;
                    if (product.Stock < 0)
                    {
                        throw new FaultException("Not enough stock remaining");
                    }
                }
                ctx.SaveChanges();
            }
        }
        public OrderDTO[] GetCustomerOrders(string customerName)
        {
            using (Model1Container ctx = new Model1Container())
            {
                Customer customer = ctx.CustomerSet.First(c => c.Name == customerName);
                Order[] ords = ctx.OrderSet.Select(o => o).Where(o => o.CustomerId == customer.Id).ToArray();
                OrderDTO[] orders = new OrderDTO[ords.Length];

                for (int i = 0; i < orders.Length; i++)
                {
                    orders[i] = createDTO(ords[i]);
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
            using (Model1Container ctx = new Model1Container())
            {
                foreach (OrderEntry ent in ord.OrderEntry)
                {
                    OrderEntryDTO entry = new OrderEntryDTO();
                    entry.Amount = ent.Amount;
                    entry.ProductID = ent.ProductId;
                    entry.ProductName = ent.Product.Name;
                    entries[i] = entry;
                    i++;
                }
            }
            order.entries = entries;
            return order;
        }
    }
}
