using PocketSharp;
using PocketSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketSharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            PocketClient client = new PocketClient(consumerKey: "34389-adecd115936b95b8e70517f0");

            List<PocketItem> items = client.Search("css").Result.ToList();

            items.ForEach(
              item => Console.WriteLine(item.ID + " | " + item.Title)
            );
        }
    }
}
