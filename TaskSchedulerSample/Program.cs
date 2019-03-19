using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the service on the remote machine
            using (TaskService ts = new TaskService())
            {
                // Create a new task definition and assign properties
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "Does something";

                var today = DateTime.Today;

                var pippo = new DailyTrigger();
                pippo.StartBoundary = new DateTime(today.Year, today.Month, today.Day-1, 12, 45, 0);

                pippo.Repetition = new RepetitionPattern(new TimeSpan(0, 10, 0), new TimeSpan(0, 30, 0), true);

                // Create a trigger that will fire the task at this time every other day
                td.Triggers.Add(pippo);

                // Create an action that will launch Notepad whenever the trigger fires
                td.Actions.Add(new ExecAction(@"D:\ProgettoYuri\PhabProjects\ProofOfConceptApps\MailgunManagement\bin\Debug\MailgunManagement.exe", null, @"D:\ProgettoYuri\PhabProjects\ProofOfConceptApps\MailgunManagement\bin\Debug"));

                //td.Settings.RunOnlyIfLoggedOn = false;

                // Register the task in the root folder
                ts.RootFolder.RegisterTaskDefinition(@"MailgunSample", td);
            }
        }
    }
}
