using Hangfire;
using Hangfire.Annotations;
using Hangfire.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Veiculo.Dominio;
using Veiculo.Dominio.ViewModel;
using Veiculo.Repositorio;

namespace Veiculos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangfireController : Controller
    {
        private readonly IBackgroundJobClient _backgroundJobs;
        private readonly VeiculoContext _context;

        public HangfireController(IBackgroundJobClient backgroundJobs, VeiculoContext context)
        {
            _backgroundJobs = backgroundJobs;
            _context = context;
        }

        //força o job a ser realizado naquele momento
        [HttpGet]
        [Route("trigger")]
        public async Task Trigger(string title)
        {
            await Task.Run(() =>
            {
                RecurringJob.Trigger(title);
            });
        }

        [HttpPost]
        [Route("new-or-update-recurrent-task")]
        public async Task RecurrentTask([FromForm] AgendadorViewModel agendador, string title, string message, string command)
        {
            //  Expression<Func<Task>> methodCall
            await Task.Run(() =>
            {
                //cria um novo AgendadorViewModel para tratamento dos campos null
                var agendadorAtt = new AgendadorViewModel()
                {
                    Minute = agendador.Minute != null ? agendador.Minute : "*",
                    Hour = agendador.Hour != null ? agendador.Hour : "*",
                    DayMonth = agendador.DayMonth != null ? agendador.DayMonth : "*",
                    Month = agendador.Month != null ? agendador.Month : "*",
                    DayWeek = agendador.DayWeek != null ? agendador.DayWeek : "*",
                };

                //transforma as propriedades do viewmodel em uma string só
                var frequencia = $"{agendadorAtt.Minute + " " + agendadorAtt.Hour + " " + agendadorAtt.DayMonth + " " + agendadorAtt.Month + " " + agendadorAtt.DayWeek}";

                //recebe os parametros
                if (command == "print")
                {
                    RecurringJob.AddOrUpdate<HangfireModule>(title, x => x.Print(message), frequencia, TimeZoneInfo.Local);
                }
                else if (command == "email")
                {
                    RecurringJob.AddOrUpdate<HangfireModule>(title, x => x.SendEmail(message), frequencia, TimeZoneInfo.Local);
                }
                else if (command == "getcar")
                {
                    RecurringJob.AddOrUpdate<CarroController>(title, x => x.Get(), frequencia, TimeZoneInfo.Local);
                }
                else if (command == "getreserva")
                {
                    RecurringJob.AddOrUpdate<ReservaController>(title, x => x.Get(), frequencia, TimeZoneInfo.Local);
                }
            });
        }

        [HttpPost]
        [Route("new-or-update-recurrent-task2")]
        public async Task RecurrentTask2([FromForm] AgendadorViewModel agendador, string title, string message, string command)
        {
            //  Expression<Func<Task>> methodCall
            await Task.Run(() =>
            {
                //cria um novo AgendadorViewModel para tratamento dos campos null
                var agendadorAtt = new AgendadorViewModel()
                {
                    Minute = agendador.Minute != null ? agendador.Minute : "*",
                    Hour = agendador.Hour != null ? agendador.Hour : "*",
                    DayMonth = agendador.DayMonth != null ? agendador.DayMonth : "*",
                    Month = agendador.Month != null ? agendador.Month : "*",
                    DayWeek = agendador.DayWeek != null ? agendador.DayWeek : "*",
                };

                //transforma as propriedades do viewmodel em uma string só
                var frequencia = $"{agendadorAtt.Minute + " " + agendadorAtt.Hour + " " + agendadorAtt.DayMonth + " " + agendadorAtt.Month + " " + agendadorAtt.DayWeek}";

                //recebe os parametros
                RecurringJob.AddOrUpdate<HangfireModule>(title, x => x.Execute(message), frequencia, TimeZoneInfo.Local);
            });
        }

        [HttpPost]
        [Route("new-schedule-task")]
        public async Task ScheduleTask(int days, string message)
        {
            await Task.Run(() =>
            {
                //não aceita id
                //programa uma tarefa para daqui a x dias
                BackgroundJob.Schedule<HangfireModule>(x => x.Print(message), TimeSpan.FromDays(days));
            });
        }

        [HttpDelete]
        [Route("delete-recurrent-task")]
        public async Task Delete(string title)
        {
            await Task.Run(() =>
            {
                RecurringJob.RemoveIfExists(title);
            });
        }


        //BackgroundJob.Enqueue(() => Console.WriteLine("Fire and Forget job"));
        //BackgroundJob.Schedule(() => Console.WriteLine("Scheduled job"), TimeSpan.FromMinutes(1));
        //RecurringJob.AddOrUpdate(() => Console.WriteLine("Recurring Job"), "* * * * *", TimeZoneInfo.Local);
        //RecurringJob.AddOrUpdate(() => new EmailService().SendEmail("Hello how are you"), "10 05 * * *", TimeZoneInfo.Local);
    }
}
