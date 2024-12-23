using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConvention
{
    public class MyClass
    {
        private const string Error = "Error";

        private readonly string _readonlyField = "Readonly Field";

        private static int Count = 0;

        private int _id;

        public string Name = "";
        public int Age;

        public int Id { get; private set; }

        public MyClass() 
        {
            _id = ++Count;
        }

        public MyClass(string name)
        {
            _id = ++Count;
            Name = name;
        }

        public int SumAllElements(int[] array)
        {
            int sum = 0;

            for(int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }

            return sum;
        }

        private void MethodB()
        {
            Console.WriteLine("This is a private method.");
        }

        protected class InnerClass
        {
            public void DisplayMessage()
            {
                Console.WriteLine("This is a inner class.");
            }
        }
    }
}
