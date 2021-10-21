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
        [HttpGet]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(int id)
        {
            try
            {
                //var user = await _context.Users.FirstOrDefaultAsync(x => x.Usuario == model.Usuario && x.Senha == model.Senha);
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return NotFound(new { message = "Usuário ou senha inválidos" });
                }

                var usersroles = await _context.UsersRoles.Where(u => u.UserId == id).Select(u => u.RoleId).ToListAsync();
                var role = _context.Roles.Where(r => usersroles.Contains(r.Id)).Select(r => r.RoleName).ToList();

                var token = ServiceToken.GenerateToken(user,role);
                user.Senha = "";
                return new
                {
                    user = user,
                    role = role,
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
