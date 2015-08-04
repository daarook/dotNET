using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Collections;

namespace ServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        void PlaceOrder(string customerName, Dictionary<string,int> orderRows);

        [OperationContract]
        OrderDTO[] GetCustomerOrders(string customerName);
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
    }
}
