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

            using (ServiceHost host1 = new ServiceHost(typeof(StoreService)))
            {
                host1.Open();
                Console.WriteLine("StoreService is running");

                Console.WriteLine("Enter command... (Type help for a list of commands)");
                while (true)
                {
                    String input = Console.ReadLine();
                    if (input == "help")
                    {
                        Console.WriteLine("testproducts -- Adds a number of testproducts to the store");
                        Console.WriteLine("exit -- shuts down the program");
                    }
                    else if (input == "testproducts")
                    {
                        //add products here
                        StoreService service = new StoreService();
                        service.addProduct("Apple", 0.24, 200);
                        service.addProduct("Mango", 3.99, 15);
                        service.addProduct("Strawberry", 0.12, 500);
                        service.addProduct("Hertog Jan 24 bottles", 13.99, 10);
                        service.addProduct("Coca Cola 1.5L", 1.75, 25);
                        Console.WriteLine("successfully added testproducts.");
                        service = null;
                    }
                    else if (input == "exit")
                    {
                        break;
                    }
                }
            }        
        }
    }
}
