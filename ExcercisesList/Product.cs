using System;
using System.Collections.Generic;
using System.Text;

namespace ExcercisesList
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int NumberOfStores { get; private set; }

        public Product(int Id, string Name)
        {
            //Console.WriteLine("Override - 1 ");
            this.Id = Id;
            this.Name = Name;
        }
        public Product(int Id, string Name, int NumberOfStores)
        {
            //Console.WriteLine("Override - 2 ");
            this.Id = Id;
            this.Name = Name;
            this.NumberOfStores = NumberOfStores;
        }
    }
}
