using System;
using System.Linq;

namespace MiscTestSample
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestExceptionFullStackTrace();

            //TestDateTimeFormatting();

            //var passPhrase = Environment.GetEnvironmentVariable("PASSPHRASE");

            TestFizzBuzz();

            Console.ReadLine();
        }


        static void TestFizzBuzz()
        {
            string FizzBuzz(int x) =>                // Local function, defined as an expression-bodied method
                (x % 3 == 0, x % 5 == 0) switch
                {    // Tuple definition  
                    (true, true) => "FizzBuzz",      // Pattern-matching on the tuple values
                    (true, _) => "Fizz",          // Discard (_) is used to omit
                    (_, true) => "Buzz",          // the values we don't care about
                    _ => x.ToString()
                };

            Enumerable.Range(1, 100)                 // Make a range of numbers from 1 to 100
                .Select(FizzBuzz).ToList()           // Map each number to a corresponding FizzBuzz value
                .ForEach(Console.WriteLine);         // Print the result to the console  
        }


        static void TestExceptionFullStackTrace()
        {
            Boolean test = true;
            //--------------b
            try
            {
                try
                {
                    if (test)
                    {
                        Console.WriteLine("test is true");
                        return;
                    }
                }
                finally
                {
                    Console.WriteLine("Executing internal finally");
                }

                TestFirstException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message: {0}", ex.Message);
                Console.WriteLine("--------------------------------------------------------------------------");
                Console.WriteLine("StackTrace: {0}", ex.StackTrace);
                Console.WriteLine("--------------------------------------------------------------------------");
                Console.WriteLine("Source: {0}", ex.Source);
                Console.WriteLine("--------------------------------------------------------------------------");
                Console.WriteLine("ToString: {0}", ex.ToString());
                Console.WriteLine("--------------------------------------------------------------------------");
            }
            finally
            {
                Console.WriteLine("Executing finally");
            }
            
        }

        static string TestNullString(string param)
        {
            string res = string.Empty;

            res = param.ToLower();

            return res;
        }

        static void TestFirstException()
        {
            //---------------a
            try
            {
                TestNullString(null);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("some message ....", ex);
            }
        }

        static void TestDateTimeFormatting()
        {
            string format = "yyyyMMdd_hhmmss_fff";
            var rightNow = DateTime.Now;

            Console.WriteLine("{0} - {1}", format, rightNow.ToString(format));

            format = "yyyy-MM-dd HH:mm:ss.FFF";

            Console.WriteLine("{0} - {1}", format, rightNow.ToString(format));

            format = "yyyy-MM-dd HH:mm:ss.fff";

            Console.WriteLine("{0} - {1}", format, rightNow.ToString(format));

            Console.ReadLine();
        }
    }
}
