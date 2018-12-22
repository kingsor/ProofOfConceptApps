using LiteDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteDbSample
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicSample();
            Console.ReadKey();
        }

        static void BasicSample()
        {
            // Open database (or create if not exits)
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                // Get customer collection
                var customers = db.GetCollection<Customer>("customers");

                // Create your new customer instance
                var customer = new Customer
                {
                    Name = "John Doe",
                    Phones = new string[] { "8000-0000", "9000-0000" },
                    IsActive = true
                };

                // Insert new customer document (Id will be auto-incremented)
                customers.Insert(customer);

                // Update a document inside a collection
                customer.Name = "Joana Doe";

                customers.Update(customer);

                // Index document using a document property
                customers.EnsureIndex(x => x.Name);

                // Use Linq to query documents
                var results = customers.Find(x => x.Name.StartsWith("Jo"));

                var json = JsonConvert.SerializeObject(results, Formatting.Indented);

                Console.WriteLine("Result:");
                Console.WriteLine(json);
            }
        }
    }
}
