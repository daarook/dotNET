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
                ErrorMessage error = new ErrorMessage();
                error.Message = "Either the user does not exist or the password is incorrect";
                error.Details = "user does not exist";
                if(ctx.CustomerSet.Any(c => c.Name == username)){
                    Customer customer = ctx.CustomerSet.Single(c => string.Equals(c.Name, username));
                    if (string.Equals(password, customer.Password))
                    {
                        return createDTO(customer);
                    }
                    error.Details = "password is incorrect";
                }
                throw new FaultException<ErrorMessage>(error, error.Details);
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
    }
}
