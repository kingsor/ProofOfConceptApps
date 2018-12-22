using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DatasetSerializationToJson
{
    class Program
    {
        static void Main(string[] args)
        {

            DataSet dataSet = GenerateSampleDataSet();

            string json = JsonConvert.SerializeObject(dataSet, Newtonsoft.Json.Formatting.Indented);

            Console.WriteLine("Sample DataSet:");
            Console.WriteLine(json);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(dataSet.GetXml());
            json = JsonConvert.SerializeXmlNode(doc).Replace("null", "\"\"").Replace("'", "\'");

            Console.WriteLine("Sample DataSet:");
            Console.WriteLine(json);

            Console.ReadKey();

        }

        static DataSet GenerateSampleDataSet()
        {
            DataSet ds = new DataSet("DataSet");

            // Table for parents
            DataTable parentTable = new DataTable("Parents");

            parentTable.Columns.Add("ParentId", typeof(int));
            parentTable.Columns.Add("ParentName", typeof(string));


            //Create some parents.
            parentTable.Rows.Add(new object[] { 1, "Parent # 1" });
            parentTable.Rows.Add(new object[] { 2, "Parent # 2" });
            parentTable.Rows.Add(new object[] { 3, "Parent # 3" });

            ds.Tables.Add(parentTable);


            // Table for childrend
            DataTable childTable = new DataTable("Childs");

            childTable.Columns.Add("ChildId", typeof(int));
            childTable.Columns.Add("ChildName", typeof(string));
            childTable.Columns.Add("ParentId", typeof(int));


            //Create some childs.
            childTable.Rows.Add(new object[] { 1, "Child # 1", 1 });
            childTable.Rows.Add(new object[] { 2, "Child # 2", 2 });
            childTable.Rows.Add(new object[] { 3, "Child # 3", 1 });
            childTable.Rows.Add(new object[] { 4, "Child # 4", 3 });
            childTable.Rows.Add(new object[] { 5, "Child # 5", 3 });

            ds.Tables.Add(childTable);


            // Create their relation.
            DataRelation parentChildRelation = new DataRelation("ParentChild", parentTable.Columns["ParentId"], childTable.Columns["ParentId"]);
            parentChildRelation.Nested = true;
            ds.Relations.Add(parentChildRelation);

            
            // Display each parent and their children based on the relation.
            foreach (DataRow parent in parentTable.Rows)
            {
                // Get children
                DataRow[] children = parent.GetChildRows(parentChildRelation);

                Console.WriteLine("\n{0}, has {1} children", parent["ParentName"].ToString(), children.Count<DataRow>());

                foreach (DataRow child in children)
                {
                    Console.WriteLine("\t{0}", child["ChildName"].ToString());
                }

            }

            return ds;
        }
    }
}
