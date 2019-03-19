using System;

namespace MiscTestSample
{
    class Program
    {
        static void Main(string[] args)
        {
            TestExceptionFullStackTrace();

            //TestDateTimeFormatting();

            var passPhrase = Environment.GetEnvironmentVariable("PASSPHRASE");

            Console.ReadLine();
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
