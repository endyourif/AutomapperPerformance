using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutoMapper;

namespace Automapper
{
    class Program
    {
        private List<Customer> _customers = new List<Customer>();

        static void Main(string[] args)
        {
            var program = new Program();
            Mapper.CreateMap<Customer, CustomerViewItem>();

            Stopwatch watch = new Stopwatch();

            for (int x = 1; x <= 100000; x *= 10)
            {
                program.PopulateCustomers(x);

                watch.Start();
                program.RunMapper(x);
                watch.Stop();
                Console.WriteLine(string.Format("AutoMapper with {0}: {1}", x, watch.ElapsedMilliseconds));

                watch.Reset();

                watch.Start();
                program.RunManual(x);
                watch.Stop();
                Console.WriteLine(string.Format("Manual Map with {0}: {1}", x, watch.ElapsedMilliseconds));
            }

            Console.ReadLine();
        }

        private void PopulateCustomers(int count)
        {
            for (int x = 0; x < count; x++)
            {
                Customer customer = GetCustomerFromDB();
                _customers.Add(customer);
            }
        }

        private void RunMapper(int count)
        {
            List<CustomerViewItem> customers = new List<CustomerViewItem>();
            foreach (Customer customer in _customers)
            {
                CustomerViewItem customerViewItem =
                    Mapper.Map<Customer, CustomerViewItem>(customer);

                customers.Add(customerViewItem);
            }
        }

        private void RunManual(int count)
        {
            List<CustomerViewItem> customers = new List<CustomerViewItem>();
            foreach (Customer customer in _customers)
            {
                CustomerViewItem customerViewItem = new CustomerViewItem()
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    DateOfBirth = customer.DateOfBirth,
                    NumberOfOrders = customer.NumberOfOrders,
                };

                customers.Add(customerViewItem);
            }
        }

        private Customer GetCustomerFromDB()
        {
            return new Customer()
            {
                DateOfBirth = new DateTime(1987, 11, 2),
                FirstName = "Andriy",
                LastName = "Buday",
                NumberOfOrders = 7
            };
        }
    }

    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int NumberOfOrders { get; set; }
    }

    public class CustomerViewItem
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int NumberOfOrders { get; set; }
    }
}
