using Microsoft.AspNetCore.Mvc;
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
        private readonly IVeiculoRepositorio _repositorio;
        public CarroController(IVeiculoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }


        // GET: api/<CarroController>
        /// <summary>
        /// Obter todos os carros.
        /// </summary>
        /// <response code="200">A lista de carros foi obtida com sucesso.</response>
        /// <response code="500">Ocorreu um erro ao obter a lista de carros.</response>        
        [HttpGet] // esse é o getAll
        [ProducesResponseType(typeof(Carro), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var carros = await _repositorio.GetAllCarros();

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
        /// <param Id="id">ID do carro.</param>
        /// <response code="200">O carro foi obtido com sucesso.</response>
        /// <response code="404">Não foi encontrado carro com ID especificado.</response>
        /// <response code="500">Ocorreu um erro ao obter o carro.</response>        
        [HttpGet("{id}", Name ="Get")] // esse aqui é o get by id !!! 
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var carros = await _repositorio.GetCarroById(id);

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
        /// <param Modelo="Modelo">Modelo do carro.</param>
        /// <response code="200">O carro foi cadastrado com sucesso.</response>
        /// <response code="400">O modelo do carro enviado é inválido.</response>
        /// <response code="500">Ocorreu um erro ao adicionar o carro.</response>
        [HttpPost]
        public async Task<IActionResult> Post(Carro model)
        {
            try
            {

                _repositorio.Add(model);
                if (await _repositorio.SaveChangeAsync()) //sempre utilizar um await, se utiliza o async
                {
                    return StatusCode((int)HttpStatusCode.Created);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }


        // PUT api/<CarroController>/5
        /// <summary>
        /// Alterar carro.
        /// </summary> 
        /// <param Id="id">ID do carro.</param>
        /// <param Modelo="Modelo">Modelo do carro.</param>
        /// <response code="200">O carro foi alterado com sucesso.</response>
        /// <response code="400">O modelo do carro enviado é inválido.</response>
        /// <response code="404">Não foi encontrado carro com ID especificado.</response>
        /// <response code="500">Ocorreu um erro ao alterar o carro.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Carro model)
        {
            try
            {
                //atualiza o carro no banco de dados
                var carro = await _repositorio.GetCarroById(id);
                if (carro != null)
                {
                    _repositorio.Update(model);
                    if (await _repositorio.SaveChangeAsync())
                    {
                        return Ok("Editado!");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        // DELETE api/<CarroController>/5
        /// <summary>
        /// Deletar carro.
        /// </summary>
        /// <param Id="id">ID do carro.</param>
        /// <response code="200">O carro foi deletado com sucesso.</response>
        /// <response code="404">Não foi encontrado carro com ID especificado.</response>
        /// <response code="500">Ocorreu um erro ao deletar o carro.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                //exclui o carro no banco de dados
                var carro = await _repositorio.GetCarroById(id);
                if (carro != null)
                {
                    _repositorio.Delete(carro);
                    if (await _repositorio.SaveChangeAsync())
                    {
                        return Ok("Deletado!");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}
