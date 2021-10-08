using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Veiculo.Dominio;
using Veiculo.Repositorio;

namespace Veiculos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarroController : ControllerBase
    {
        private readonly VeiculoContext _context;      
        public CarroController(VeiculoContext context)
        {
            _context = context;
        }

        // GET: api/<CarroController>
        /// <summary>
        /// Obter todos os carros.
        /// </summary>               
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var carros = await _context.Carros.ToListAsync();
                return Ok(carros);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // GET api/<CarroController>/5
        /// <summary>
        /// Obter um carro específico por ID.
        /// </summary>              
        [HttpGet("{id:int}")] //delimita a rota, da erro 404 se colocar um valor q não seja int
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var carros = await _context.Carros.FirstOrDefaultAsync(x => x.Id == id);
                //.AsNoTracking()

                return Ok(carros);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Carro não encontrado.");
        }


        // POST api/<CarroController>
        /// <summary>
        /// Cadastrar carro.
        /// </summary>        
        [HttpPost]
        public ActionResult Post(Carro model)
        {
            try
            {
                //adiciona o carro no banco de dados
                var carro = new Carro();
                _context.Carros.Add(model);
                _context.SaveChanges();
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }


        // PUT api/<CarroController>/5
        /// <summary>
        /// Alterar carro.
        /// </summary>         
        [HttpPut("{id}")]
        public IActionResult Put(int id, Carro model)
        {
            try
            {
                //atualiza o carro no banco de dados
                var carro = new Carro();
                _context.Carros.Update(model);
                _context.SaveChanges();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // DELETE api/<CarroController>/5
        /// <summary>
        /// Deletar carro.
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                //deleta o carro no banco de dados
                var carro = _context.Carros.Where(carro => carro.Id == id).Single();

                if (carro != null)
                {
                    _context.Carros.Remove(carro);
                    _context.SaveChanges();
                    return Ok("Deletado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Carro não encontrado.");
        }
    }
}
