using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceLibrary
{
    public class CustomerService : ICustomerService
    {
        public string register(string name)
        {
            using(Model1Container ctx = new Model1Container()) {
                if (ctx.CustomerSet.Any(c => c.Name == name))
                {
                    char[] charArray = name.ToCharArray();
                    Array.Reverse(name.ToCharArray());
                    string password = new string(charArray);
                    Customer customer = new Customer { Name = name, Password = password};
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
        public bool authenticate(string username, string password)
        {
            using (Model1Container ctx = new Model1Container())
            {
                return string.Equals(password,((Customer)ctx.CustomerSet.Select(c => c).Where(c => string.Equals(c.Name , username))).Password);
            }
        }
    }
}
