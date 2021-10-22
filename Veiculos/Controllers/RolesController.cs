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
    public class RolesController : ControllerBase
    {
        private readonly VeiculoContext _context;
        public RolesController(VeiculoContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Obter todas as roles.
        /// </summary>               
        [HttpGet]
        [Authorize(Roles = "manager,HR")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var roles = await _context.Roles.ToListAsync();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        /// <summary>
        /// Obter uma role específica por ID.
        /// </summary>              
        [HttpGet("{id:int}")]
        [Authorize(Roles = "manager,HR")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var roles = await _context.Roles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        /// <summary>
        /// Cadastrar role.
        /// </summary>        
        [HttpPost]
        [Authorize(Roles = "manager,HR")]
        public ActionResult Post(Role model)
        {
            try
            {
                _context.Roles.Add(model);
                _context.SaveChanges();
                return Ok("Nova role adicionada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }        

        /// <summary>
        /// Alterar role.
        /// </summary>         
        [HttpPut("{id}")]
        [Authorize(Roles = "manager,HR")]
        public async Task<IActionResult> Put(int id, Role model)
        {
            try
            {
                var role = await _context.Roles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                var roleAtt = new Role()
                {
                    Id = role.Id,                    
                    RoleName = model.RoleName != null ? model.RoleName : role.RoleName,
                };

                if (roleAtt != null)
                {
                    _context.Roles.Update(roleAtt);
                    _context.SaveChanges();
                    return Ok("Role editada com sucesso!");
                }
                else
                {
                    return Ok("Role não encontrada");
                }                
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }


        //    // Retrieve all the Users
        //    foreach (var user in userManager.Users)
        //    {
        //        // If the user is in this role, add the username to
        //        // Users property of EditRoleViewModel. This model
        //        // object is then passed to the view for display
        //        if (await userManager.IsInRoleAsync(user, role.Name))
        //        {
        //            model.Users.Add(user.UserName);
        //        }
        //    }

        //    return View(model);
        //}

        /// <summary>
        /// Deletar role.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "manager")]
        public ActionResult Delete(int id)
        {
            try
            {
                var role = _context.Roles.Where(u => u.Id == id).Single();
                if (role != null)
                {
                    _context.Roles.Remove(role);
                    _context.SaveChanges();
                    return Ok("Role deletada com sucesso!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Role não encontrada.");
        }

    }
}
