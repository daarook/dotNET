using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ServiceLibrary;

namespace StoreHost
{
    class Program
    {
        static void Main(string[] args)
        {
            //not decided to use 'using' because multiple services need to be running
            ServiceHost host1 = new ServiceHost(typeof(CustomerService));
            ServiceHost host2 = new ServiceHost(typeof(OrderService));
            ServiceHost host3 = new ServiceHost(typeof(ProductService));

            host1.Open();
            Console.WriteLine("CustomerService active");
            host2.Open();
            Console.WriteLine("OrderService active");
            host3.Open();
            Console.WriteLine("ProductService active");

            Console.WriteLine("Press any key to close server");
            Console.ReadKey();
        
        }
    }
}
