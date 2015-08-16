using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService2" in both code and config file together.
    [ServiceContract]
    public interface IStoreService
    {
        [OperationContract]
        string Register(String name);

        [OperationContract]
        CustomerDTO Authenticate(string username, string password);

        [OperationContract]
        ProductDTO[] GetProductsInStock();

        [OperationContract]
        void addProduct(string name, double price, int stock);

        [OperationContract]
        void PlaceOrder(string customerName, Dictionary<string, int> orderRows);

        [OperationContract]
        OrderDTO[] GetCustomerOrders(string customerName);
    }

    //TODO DTO objecten om te versturen over de lijn
    [DataContract]
    public class CustomerDTO
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public double Saldo { get; set; }
    }

    [DataContract]
    public class ProductDTO
    {
        [DataMember]
        public int Stock { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public double Price { get; set; }
    }

    [DataContract]
    public class OrderDTO
    {
        [DataMember]
        public DateTime OrderDate { get; set; }

        [DataMember]
        public int CustomerID { get; set; }

        [DataMember]
        public OrderEntryDTO[] entries { get; set; }

    }

    [DataContract]
    public class OrderEntryDTO
    {
        [DataMember]
        public int Amount { get; set; }

        [DataMember]
        public int ProductID { get; set; }

        [DataMember]
        public string ProductName { get; set; }
    }
}
