using ExcelDataReader;
using System;
using System.Data;
using System.IO;
using System.Text;

namespace ImportExcelToScript
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var filePath = @"D:\ProgettoYuri\DB-Comuni\LIGHT\italy_cities.xlsx";

            DataSet excelContent = ReadAsDataSet(filePath);

            Console.WriteLine(String.Format("# of Tables: {0}", excelContent.Tables.Count));

            Console.WriteLine(String.Format("TableName: {0}", excelContent.Tables[0].TableName));

            StringBuilder sb = new StringBuilder();

            foreach (DataColumn col in excelContent.Tables[0].Columns)
            {
                Console.WriteLine(String.Format("Column Type: {0}", col.DataType.ToString()));
            }

            bool excludeFirst = true;

            foreach(DataRow row in excelContent.Tables[0].Rows)
            {
                if(excludeFirst)
                {
                    excludeFirst = false;
                    continue;
                }

                sb.Append("INSERT INTO Y_CITIES VALUES(");

                for (int idx=0; idx< excelContent.Tables[0].Columns.Count; idx++)
                {
                    sb.AppendFormat(idx==0? "{0}":idx<7?",'{0}'":",{0}", row[idx].ToString().Replace("'", "''"));
                }
                sb.AppendLine(");");
            }

            File.WriteAllText("output.sql", sb.ToString());

            Console.WriteLine(String.Format("# of Rows: {0}", excelContent.Tables[0].Rows.Count));

            Console.ReadKey();

        }

        static DataSet ReadAsDataSet(string filePath)
        {
            DataSet res = new DataSet();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {

                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateReader(stream, new ExcelReaderConfiguration() { FallbackEncoding = Encoding.UTF8 }))
                {

                    // 2. Use the AsDataSet extension method
                    res = reader.AsDataSet();

                    // The result of each spreadsheet is in result.Tables
                }
            }

            return res;
        }
    }
}
