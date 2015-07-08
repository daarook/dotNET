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
    public interface IProductService
    {
        [OperationContract]
        void ChangeProductStock(Product product, int amount);

        [OperationContract]
        ProductDTO[] GetProductsInStock(); 
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
}
