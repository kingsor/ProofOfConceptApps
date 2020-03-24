using NLog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadingSample
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            logger.Info("Starting Threading Sample");

            //create task object and pass anonymous method 
            //to task constructor parameter as work to do 
            //within the task
            Task t = new Task(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    //print task t thread id
                    var threadId = Thread.CurrentThread.ManagedThreadId;
                    Console.WriteLine("Task Loop Current Thread Id:" + threadId);
                    logger.Info("Printing index: {0}", i);
                }
            });

            //start task t execution
            t.Start();

            LogManager.Configuration.Variables["logDirectory"] = @"C:\NeetpiqLab\logs";

            for (int i = 0; i < 100; i++)
            {
                //print main thread id
                var threadId = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine("Main Loop Current Thread Id " + threadId);
                logger.Info("Printing index: {0}", i);
            }

            //wait for task t to complete its execution
            t.Wait();

            logger.Info("Completed Threading Sample");

            Console.WriteLine("Press enter terminate the process!");
            Console.ReadLine();
        }
    }
}
