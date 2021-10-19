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
    public class AuthenticateController : ControllerBase
    {
        private readonly VeiculoContext _context;
        public AuthenticateController(VeiculoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obter token de autenticação.
        /// </summary> 
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Usuario == model.Usuario && x.Senha == model.Senha);

                if (user == null)
                {
                    return NotFound(new { message = "Usuário ou senha inválidos" });
                }

                var token = ServiceToken.GenerateToken(user);
                user.Senha = "";
                return new
                {
                    user = user,
                    token = token
                };

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }
    }
}
