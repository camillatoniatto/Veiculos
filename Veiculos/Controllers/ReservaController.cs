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
    public class ReservaController : ControllerBase
    {
        private readonly VeiculoContext _context;
        public ReservaController(VeiculoContext context)
        {
            _context = context;
        }


        // GET: api/<ReservaController>
        /// <summary>
        /// Obter todas as reservas.
        /// </summary>  
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var reserva = await _context.Reservas.ToListAsync();
                return Ok(reserva);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // GET api/<ReservaController>/5
        /// <summary>
        /// Obter uma reserva específica por ID.
        /// </summary>    
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var reservas = await _context.Reservas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                return Ok(reservas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // POST api/<ReservaController>
        /// <summary>
        /// Cadastrar reserva.
        /// </summary>   
        [HttpPost]
            public ActionResult Post(Reserva model)
            {
                try
                {
                    var reserva = new Reserva();
                    _context.Reservas.Add(model);
                    _context.SaveChanges();
                    return Ok("Ok");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro: {ex}");
                }
            }

        // PUT api/<ReservaController>/5
        /// <summary>
        /// Alterar reserva.
        /// </summary> 
        [HttpPut("{id}")]
        public IActionResult Put(int id, Reserva model)
        {
            try
            {
                var reserva = new Reserva();
                _context.Reservas.Update(model);
                _context.SaveChanges();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // DELETE api/<ReservaController>/5
        /// <summary>
        /// Deletar reserva.
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var reserva = _context.Reservas.Where(reserva => reserva.Id == id).Single();

                if (reserva != null)
                {
                    _context.Reservas.Remove(reserva);
                    _context.SaveChanges();
                    return Ok("Deletado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Reserva não encontrada.");
        }
    }
}
