using lambda1_csharp.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace lambda1_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();
            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    List<Product> list = new List<Product>();
                    while (!sr.EndOfStream)
                    {
                        string[] products = sr.ReadLine().Split(',');
                        string name = products[0];
                        double price = double.Parse(products[1], CultureInfo.InvariantCulture);
                        list.Add(new Product(name, price));
                    }

                    var average = list.Select(x => x.Price).DefaultIfEmpty(0.0).Average();
                    Console.WriteLine("Average price = " + average.ToString("F2", CultureInfo.InvariantCulture));

                    var names = list.Where(p => p.Price < average).OrderByDescending(p => p.Name).Select(p => p.Name);
                    foreach (string name in names)
                    {
                        Console.WriteLine(name);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(e.Message);
            }
        }
    }
}