﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceLibrary
{
    public class CustomerService : ICustomerService
    {
        public string Register(string name)
        {
            using(Model1Container ctx = new Model1Container()) {
                if (ctx.CustomerSet.Any(c => c.Name == name))
                {
                    char[] charArray = name.ToCharArray();
                    Array.Reverse(name.ToCharArray());
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
        public Customer Authenticate(string username, string password)
        {
            using (Model1Container ctx = new Model1Container())
            {
                if(ctx.CustomerSet.Any(c => c.Name == username)){
                    Customer customer = ctx.CustomerSet.Single(c => string.Equals(c.Name, username));
                    if (string.Equals(password, customer.Password))
                    {
                        return customer;
                    }
                }            
                throw new FaultException("customer does not exist or wrong password");
            }
        }
    }
}
