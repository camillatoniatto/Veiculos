using Hangfire;
using Hangfire.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Veiculos
{

    public class HangfireLogic
    {

        public void RunNow()
        {
            BackgroundJob.Enqueue<HangfireModule>(x => x.PrintToConsole());
        }


        public void RunOnSchedule()
        {
            RecurringJob.AddOrUpdate<HangfireModule>(x => x.PrintToConsole(), "* * * * *", TimeZoneInfo.Local);
        }

        public void RunOnScheduleIded()
        {
            RecurringJob.AddOrUpdate<HangfireModule>("testeId", x => x.PrintToConsole(), "* * * * *", TimeZoneInfo.Local);
        }

        public void Remove()
        {
            RecurringJob.RemoveIfExists("testeId");
        }

    }


    //class com as funções 
    public class HangfireModule
    {

        public async Task Print(string message)
        {
            await Task.Run(() =>
            {
                Console.WriteLine(message);
            });
        }

        public async Task PrintToConsole()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("teste");
            });
        }

        public void SendEmail(string message)
        {

            Console.WriteLine("Email sent with message: " + message);
        }
    }

}
