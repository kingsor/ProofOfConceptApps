using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOX;

namespace ConsoleAppNetFramework
{
    class Program
    {
        static string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=Database.mdb";

        static void Main(string[] args)
        {
            CreateAccessDatabase();
            
            OpenAccessDatabase();

            Console.WriteLine("Press Enter to finish.");
            Console.ReadLine();
        }

        static void CreateAccessDatabase()
        {
            Catalog catalog = new Catalog();
            catalog.Create(ConnectionString);
            catalog = null;

        }

        static void OpenAccessDatabase()
        {
            OleDbConnection myConnection = new OleDbConnection(ConnectionString);
            myConnection.Open();
            OleDbCommand myCommand = new OleDbCommand();
            myCommand.Connection = myConnection;
            myCommand.CommandText = "CREATE TABLE table1([KEY] Text, [VALUE] Text)";
            myCommand.ExecuteNonQuery();
            myCommand.Connection.Close();
        }
    }
}
