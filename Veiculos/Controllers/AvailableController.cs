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
        public async Task<IActionResult> GetAvailableCars(DateTime DtInicio, DateTime DtFim)
        {
            if (DtInicio < DtFim)
            {
                try
                {             
                    var listaReservas = await _context.Reservas.Where(d => d.DtInicio >= DtInicio && d.DtFim <= DtFim).ToListAsync(); //verifica os veiculos reservados em x data
                    var carrosReservados = listaReservas.Select(r => r.CarroId); //pega carroId dos veiculos reservados em x data
                    var carrosAvailable = _context.Carros.Where(c => !carrosReservados.Contains(c.Id) && c.Estado == "available").ToList(); //compara carroId com Id dos veiculos restantes (IsNotIn)

                    return Ok(carrosAvailable);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro: {ex}");
                }
            }
            return Ok("Data inválida");
        }
    }
}