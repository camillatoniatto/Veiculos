using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "manager,mechanic,support,client")]
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
        [Authorize(Roles = "manager,mechanic,support,client")]
        //[AllowAnonymous]
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
        [Authorize(Roles = "manager,mechanic,support")]
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
        }


        // POST api/<CarroController>
        /// <summary>
        /// Cadastrar carro.
        /// </summary>        
        [HttpPost]
        [Authorize(Roles = "manager,mechanic,support")]
        public ActionResult Post(Carro model)
        {
            try
            {
                //adiciona o carro no banco de dados
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
        [Authorize(Roles = "manager,mechanic")]
        public async Task<IActionResult> Put(int id, Carro model)
        {
            try
            {                
                //atualiza o carro no banco de dados
                var carro = await _context.Carros.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                var carroAtt = new Carro()


                /*
                foreach (var item in model)
                {
                    if (item != null)
                    {
                        carroAtt.Add(item);
                    }
                    else
                    {
                        carroAtt.Add(item.carro);
                    }
                }*/


                {
                    Id = carro.Id,
                    Modelo = model.Modelo != null ? model.Modelo : carro.Modelo,
                    Ano = model.Ano != 0 ? model.Ano : carro.Ano,
                    Placa = model.Placa != null ? model.Placa : carro.Placa,
                    Estado = model.Estado != null ? model.Estado : carro.Estado,
                };

                if (carroAtt != null)
                {
                    _context.Carros.Update(carroAtt);
                    _context.SaveChanges();
                    return Ok("Editado com sucesso!");
                }
                return Ok(carroAtt);
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
        [Authorize(Roles = "manager")]
        public ActionResult Delete(int id)
        {
            var listaReservas = _context.Reservas.Where(d => d.CarroId == id).ToList();

            if (listaReservas.Count() == 0)
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
            return Ok("Você não pode deletar um carro que está reservado");  
        }
    }
}
