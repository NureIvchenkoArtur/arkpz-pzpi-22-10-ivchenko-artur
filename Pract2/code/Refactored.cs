using Pract2.NotRefactored;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract2.Refactored
{
    public class Order
    {
        public List<Item> Items { get; set; }
        public bool IsValid { get; set; }
        public bool HasItems { get; set; }

        //Refactoring Method 1
        public void ProcessOrder(Order order)
        {
            if (order == null)
            {
                Console.WriteLine("Order is null.");
                return;
            }

            if (!order.IsValid)
            {
                Console.WriteLine("Order is invalid.");
                return;
            }

            if (!order.HasItems)
            {
                Console.WriteLine("Order has no items.");
                return;
            }

            Process(order);
        }

        public void Process(Order order)
        {
            Console.WriteLine("Processing...");
        }

    }

    //Refactoring Method 2
    public class OrderTotalCalculator
    {
        private readonly Order _order;
        private readonly double _discount;
        private readonly double _taxRate;

        public OrderTotalCalculator(Order order, double discount, double taxRate)
        {
            _order = order;
            _discount = discount;
            _taxRate = taxRate;
        }

        public double CalculateTotal()
        {
            double subtotal = CalculateSubtotal();
            double discountAmount = subtotal * _discount;
            double taxAmount = (subtotal - discountAmount) * _taxRate;
            return subtotal - discountAmount + taxAmount;
        }

        private double CalculateSubtotal()
        {
            double subtotal = 0;
            foreach (var item in _order.Items)
            {
                subtotal += item.Price * item.Quantity;
            }
            return subtotal;
        }
    }

    //Refactoring Method 3
    public class Employee
    {
        private string _name;
        private double _salary;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public double Salary
        {
            get => _salary;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Salary cannot be negative.");
                }
                _salary = value;
            }
        }
    }

    public class Item
    {
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
