using Hangfire;
using Hangfire.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veiculo.Dominio;
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

        //[HttpGet]
        //public async Task GetAllCars()
        //{
        //    await Task.Run(() =>
        //    {

        //        RecurringJob.AddOrUpdate<CarroController>("All Cars", x => x.Get(), "00 19 5 * *", TimeZoneInfo.Local);
        //        //RecurringJob.
        //    });

        [HttpPost]
        [Route("new-or-update-task")]
        public async Task NewTask([FromForm] Agendador agendador, string title, string message)
        {
            
            await Task.Run(() =>
            {
                _context.Agendadores.Add(agendador);
                var frequencia = agendador.Minute + agendador.Hour + agendador.DayMonth + agendador.Month + agendador.DayWeek;
                RecurringJob.AddOrUpdate<HangfireModule>(title, x => x.Print(message), frequencia, TimeZoneInfo.Local);
                _context.SaveChanges();
            });
        }

        [HttpDelete]
        [Route("delete-task")]
        public async Task Delete(string title)
        {
            await Task.Run(() =>
            {
                RecurringJob.RemoveIfExists(title);
            });            
        }

        //public IActionResult Index()
        //{
        //    HangfireLogic hangfireLogic = new HangfireLogic();
        //    hangfireLogic.RunNow();
        //    ViewBag.Title = "Home Page";

        //    //HangfireLogic hangfireLogic = new HangfireLogic();
        //    //hangfireLogic.RunOnSchedule();
        //    //ViewBag.Title = "Home Page";

        //    //HangfireLogic hangfireLogic = new HangfireLogic();
        //    //hangfireLogic.RunOnScheduleIded();
        //    //ViewBag.Title = "Home Page";


        //    //_backgroundJobs.Enqueue(() => Console.WriteLine("Fire and Forget job"));
        //    //BackgroundJob.Enqueue(() => Console.WriteLine("Fire and Forget job"));

        //    //BackgroundJob.Schedule(() => Console.WriteLine("Scheduled job"), TimeSpan.FromMinutes(1));

        //    //RecurringJob.AddOrUpdate(() => Console.WriteLine("Recurring Job"), "* * * * *", TimeZoneInfo.Local);

        //    ////RecurringJob.AddOrUpdate(() => new EmailService().SendEmail("Hello how are you"), "10 05 * * *", TimeZoneInfo.Local);


        //    return View();
        //}
    }
}
