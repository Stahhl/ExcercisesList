using System;
using System.Collections.Generic;
using System.Linq;
//https://stackoverflow.com/questions/26752909/replace-a-object-in-a-list-of-objects
namespace ExcercisesList
{
    class Program
    {
        static ConsoleColor currentColor;
        static List<string> Module_10_4_List;
        static List<double> Module_10_4_Extra_List;
        static List<Product> Module_10_5_List;

        static void Main()
        {
            //Module_10_4();
            //Module_10_4_Extra();
            Module_10_5();
        }
        static void Module_10_4()
        {
            if (Module_10_4_List == null)
                Module_10_4_List = new List<string>();

            while (Module_10_4_Loop())
            {

            }
            Console.WriteLine();
            Module_10_4_List.Sort();
            if(Module_10_4_List.Count >= 2)
            {
                Module_10_4_List.RemoveAt(0);
                Module_10_4_List.RemoveAt(Module_10_4_List.Count - 1);
            }
            foreach (string name in Module_10_4_List)
            {
                Console.Write("\n" + name);
            }
            Restart();
        }
        static bool Module_10_4_Loop()
        {
            Console.Write("Enter a name: ");
            string input = Console.ReadLine();

            if(input.ToLower() == "quit")
                return false;

            Module_10_4_List.Add(input);
            return true;
        }
        static void Module_10_4_Extra()
        {
            if (Module_10_4_Extra_List == null)
                Module_10_4_Extra_List = new List<double>();
            while (Module_10_4_Extra_Loop())
            {

            }
            Console.WriteLine();
            Module_10_4_Extra_List.Sort();
            int middle = Module_10_4_Extra_List.Count - (Module_10_4_Extra_List.Count / 2);
            double mean = (Module_10_4_Extra_List.Sum() / Module_10_4_Extra_List.Count);
            double median = Module_10_4_Extra_List[middle - 1];
            Console.WriteLine("mean: " + mean);
            Console.WriteLine("median: " + median);
        }
        static bool Module_10_4_Extra_Loop()
        {
            Console.Write("Enter a number: ");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                return false;

            if (double.TryParse(input, out double result) == true)
                Module_10_4_Extra_List.Add(result);
            else
                Console.WriteLine("Could not parse input: " + input);

            return true;
        }
        static void Module_10_5()
        {
            if (Module_10_5_List == null)
                Module_10_5_List = new List<Product>();

            while (Module_10_5_Loop())
            {

            }
            SetTextColor(ConsoleColor.White);

            Console.WriteLine("\nContent: \n");

            foreach (Product product in Module_10_5_List)
            {
                Console.WriteLine($"Product id = {product.Id} and name = {product.Name} and number of stores = {product.NumberOfStores}");
            }
            Restart();
        }
        static bool Module_10_5_Loop()
        {
            SetTextColor(ConsoleColor.White);
            Console.Write("Enter a product (id,name,extra): ");
            SetTextColor(ConsoleColor.Green);
            string input = Console.ReadLine();

            if (input == string.Empty)
                return false;

            string[] stringArray = input.Split(',');

            if (Int32.TryParse(stringArray[0], out int result) == false || 
                stringArray.Length < 2 || stringArray.Length > 4)
            {
                PrintRedMessage("Invalid input! ");
            }

            for (int i = 0; i < stringArray.Length; i++)
            {
                stringArray[i].Trim();
                string[] commandArray = stringArray[i].Split(':');
                if (commandArray.Length > 1 && commandArray[0].ToUpper() == "COMMAND")
                {
                    DoCommand(commandArray[1].ToUpper(), stringArray);
                }
            }
            //var products = from p in Module_10_5_List
            CreateProduct(stringArray);

            return true;
        }
        static void CreateProduct(string[] stringArray)
        {
            int result = Int32.Parse(stringArray[0]);

            if(RemoveProduct(false, stringArray) == true)
            {
                PrintRedMessage("Id already exists replace the old one y/n ?: ");
                if (Console.ReadLine().ToUpper() == "Y")
                {
                    RemoveProduct(true, stringArray);
                }
            }

            //Calls a method that calls the correct overload constructor for 'Product' class
            if (stringArray.Length == 2)
                StringArray_2(result, stringArray);
            if (stringArray.Length == 3)
                StringArray_3(result, stringArray);
        }
        static void StringArray_2(int id, string[] stringArray)
        {
            //User entered (int)id,(string)name
            Module_10_5_List.Add(new Product(id, stringArray[1]));
        }
        static void StringArray_3(int id, string[] stringArray)
        {
            bool parsed = Int32.TryParse(stringArray[2], out int nbStores);
            //user entered (int)id,(string)name,(int)nbStores
            //or           (int)id,(string)name,(int)nbStores,(string)command
            if (parsed == true)
                Module_10_5_List.Add(new Product(id, stringArray[1], nbStores));
            //User entered (int)id,(string)name,(string)command
            else
                Module_10_5_List.Add(new Product(id, stringArray[1]));
        }
        static void Restart()
        {
            Console.WriteLine();
            Console.ReadKey();
            Console.Clear();
            Main();
        }
        static void PrintRedMessage(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(msg);
            Console.ForegroundColor = currentColor;
        }
        static void SetTextColor(ConsoleColor color)
        {
            currentColor = color;
            Console.ForegroundColor = currentColor;
        }
        static bool RemoveProduct(bool delete, string[] stringArray)
        {
            //Check if a product with the same id already exists 
            //If true and parameter 'delete is:
            //true - delete the old product
            //false - return true to tell the user that id already exists
            int id = Int32.Parse(stringArray[0]);
            List<Product> products = Module_10_5_List.OrderBy(x => x.Id == id).ToList();

            if(products.Count() > 0 && delete == true)
            {
                for (int i = 0; i < products.Count(); i++)
                {
                    //Console.WriteLine($"Removed product id: {products[i].Id} name: {products[i].Name}");
                    Module_10_5_List.Remove(products[i]);
                    return false;
                }
            }
            if(products.Count() > 0 && delete == false)
            {
                return true;
            }
            return false;
        }
        static string[] Capitalize(string[] stringArray)
        {
            //Capitalize the first letter in products name
            stringArray[1] = stringArray[1].ToUpper();
            return stringArray;
        }
        static void DoCommand(string c, string[] stringArray)
        {
            string[] commands = c.Split("+");

            foreach (string cmd in commands)
            {
                switch (cmd)
                {
                    case "REPLACE":
                        RemoveProduct(true, stringArray);
                        break;
                    case "TOUPPER":
                        stringArray = Capitalize(stringArray);
                        break;
                }
            }
        }
    }
}
