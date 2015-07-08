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
    public interface ICustomerService
    {
        [OperationContract]
        string Register(String name);

        [OperationContract]
        [FaultContract(typeof(ErrorMessage))]
        CustomerDTO Authenticate(string username, string password);
    }

    [DataContract]
    public class ErrorMessage
    {
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string Details { get; set; }
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
}
