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

        public void RunNow(string message)
        {
            BackgroundJob.Enqueue<HangfireModule>(x => x.Execute(message));
        }

        public void RunOnSchedule(string message)
        {
            RecurringJob.AddOrUpdate<HangfireModule>(x => x.Execute(message), "* * * * *", TimeZoneInfo.Local);
        }

        public void RunOnScheduleIded(string message)
        {
            RecurringJob.AddOrUpdate<HangfireModule>("testeId", x => x.Execute(message), "* * * * *", TimeZoneInfo.Local);
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

        public async Task Execute(string message)
        {
            await Task.Run(() =>
            {
                Console.WriteLine(message);
            });
        }

        public void SendEmail(string message)
        {

            Console.WriteLine("Email sent with message: " + message);
        }
    }

}
