using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        void ChangeProductStock(Product product, int amount);

        [OperationContract]
        Product[] GetProductsInStock(); 
    }
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "ServiceLibrary.ContractType".
    //[DataContract]
    //public class Product
    //{
    //    int stock;
    //    string name;
    //    double price;
    //    [DataMember]
    //    public int Stock
    //    {
    //        get { return stock; }
    //        set { stock = value; }
    //    }

    //    [DataMember]
    //    public string Name
    //    {
    //        get { return name; }
    //        set { name = value; }
    //    }

    //    [DataMember]
    //    public double Price
    //    {
    //        get { return price; }
    //        set { price = value; }
    //    }
    //}
}
