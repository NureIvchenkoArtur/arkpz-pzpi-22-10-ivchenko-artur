using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract2.NotRefactored
{
    public class Order
    {
        public List<Item> Items { get; set; }
        public bool IsValid { get; set; }
        public bool HasItems { get; set; }

        //Refactoring Method 1
        public void ProcessOrder(Order order)
        {
            if (order != null)
            {
                if (order.IsValid)
                {
                    if (order.HasItems)
                    {
                        Process(order);
                    }
                    else
                    {
                        Console.WriteLine("Order has no items.");
                    }
                }
                else
                {
                    Console.WriteLine("Order is invalid.");
                }
            }
            else
            {
                Console.WriteLine("Order is null.");
            }
        }

        public void Process(Order order)
        {
            Console.WriteLine("Processing...");
        }

        //Refactoring Method 2
        public double CalculateOrderTotal(Order order, double discount, double taxRate)
        {
            double subtotal = 0;
            foreach (var item in order.Items)
            {
                subtotal += item.Price * item.Quantity;
            }

            double discountAmount = subtotal * discount;
            double taxAmount = (subtotal - discountAmount) * taxRate;
            return subtotal - discountAmount + taxAmount;
        }
    }

    //Refactoring Method 3
    public class Employee
    {
        public string Name;
        public double Salary;
    }

    public class Item
    {
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
