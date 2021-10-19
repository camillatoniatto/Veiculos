using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class UserController : ControllerBase
    {
        private readonly VeiculoContext _context;
        public UserController(VeiculoContext context)
        {
            _context = context;
        }

        // GET: api/<UserController>
        /// <summary>
        /// Obter todos os funcionários.
        /// </summary>               
        [HttpGet]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // POST api/<UserController>
        /// <summary>
        /// Cadastrar funcionário.
        /// </summary>        
        [HttpPost]
        [Authorize(Roles = "manager")]
        public ActionResult Post(User model)
        {
            try
            {
                _context.Users.Add(model);
                _context.SaveChanges();
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }


    }
}
