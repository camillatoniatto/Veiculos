using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veiculo.Dominio;
using Veiculo.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Veiculos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailableController : ControllerBase
    {
        private readonly VeiculoContext _context;

        public AvailableController(VeiculoContext context)
        {
            _context = context;
        }
        // GET: api/<AvailableController>
        /// <summary>
        /// Verificar veículos disponíveis.
        /// </summary>  
        [HttpGet]
        public async Task<IActionResult> Get(DateTime DtInicio, DateTime DtFim, Reserva model)
        {
            try
            {               
                var data = await _context.Reservas.AsNoTracking().Where(d => d.DtInicio > DtInicio && d.DtFim > DtFim).ToListAsync();
                //if (data)
               // {
                    return Ok(data); //model
               // }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }
    }
}



//where a.Id == p.OID &&
//       (a.Start.Date >= startDate.Date && a.Start.Date <= endDate)
// .Where(x => x.CarroId == id)
//var reservas = await _context.Reservas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);