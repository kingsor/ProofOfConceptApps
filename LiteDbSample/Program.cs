using LiteDB;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace LiteDbSample
{
    class Program
    {
        static void Main(string[] args)
        {
            //BasicSample();k
            LoadPocketExport();
            
            Console.ReadKey();
        }

        static void LoadPocketExport()
        {
            var filename = @".\pocket-export.json";

            var json = File.ReadAllText(filename);

            var pocketList = JArray.Parse(json);

            var count = 0;

            foreach(var item in pocketList)
            {
                var url = item["url"].ToString();
                var dateSaved = Convert.ToInt64( item["time_added"].ToString());

                var date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    .AddMilliseconds(dateSaved * 1000);

                Console.WriteLine("{0} - {1} - {2}", dateSaved, date, date.ToLocalTime());

                count++;
                if (count >= 10)
                    break;
            }

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
