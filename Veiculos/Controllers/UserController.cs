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
        /// Obter todos os usuários.
        /// </summary>               
        [HttpGet]
        //[Authorize(Roles = "manager")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                //users.Senha = "";
                
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // GET api/<UserController>/5
        /// <summary>
        /// Obter um usuário específico por ID.
        /// </summary>              
        [HttpGet("{id:int}")] //delimita a rota, da erro 404 se colocar um valor q não seja int
        //[Authorize(Roles = "manager,support")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var users = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                users.Senha = "";
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // POST api/<UserController>
        /// <summary>
        /// Cadastrar usuário.
        /// </summary>        
        [HttpPost]
        //[Authorize(Roles = "manager,support")]
        public ActionResult Post(User model)
        {
            try
            {
                _context.Users.Add(model);
                _context.SaveChanges();
                return Ok("Usuário cadastrado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // PUT api/<UserController>/5
        /// <summary>
        /// Alterar usuário.
        /// </summary>         
        [HttpPut("{id}")]
        //[Authorize(Roles = "manager,support")]
        public async Task<IActionResult> Put(int id, User model)
        {
            try
            {
                var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                var userAtt = new User()
                {
                    Id = user.Id,
                    Usuario = model.Usuario != null ? model.Usuario : user.Usuario,
                    Senha = model.Senha != null ? model.Senha : user.Senha,                    
                };

                if (userAtt != null)
                {
                    _context.Users.Update(userAtt);
                    _context.SaveChanges();
                    return Ok("Usuário editado com sucesso!");
                }
                return Ok(userAtt);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // DELETE api/<UserController>/5
        /// <summary>
        /// Deletar usuário.
        /// </summary>
        [HttpDelete("{id}")]
        //[Authorize(Roles = "manager")]
        public ActionResult Delete(int id)
        {           
            try
            {
                var user = _context.Users.Where(u => u.Id == id).Single();
                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                    return Ok("Usuário deletado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        return BadRequest("Usuário não encontrado.");           
        }
    }
}
