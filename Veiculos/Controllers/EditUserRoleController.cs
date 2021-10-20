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
    public class EditUserRoleController : ControllerBase
    {
        private readonly VeiculoContext _context;

        public EditUserRoleController(VeiculoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Nova relação user e role.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostUserRole(UserRole model)
        {
            var user = await _context.Users.Where(u => u.Id == model.UserId).ToListAsync();
            if (user != null)
            {
                var role = await _context.Roles.Where(r => r.Id == model.RoleId).ToListAsync();
                if (role != null)
                {
                    var usersroles = _context.UsersRoles.Find(model.UserId, model.RoleId);
                    if (usersroles == null)
                    {
                        try
                        {
                            _context.UsersRoles.Add(model);
                            _context.SaveChanges();
                            return Ok("Nova relação adicionada com sucesso");
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                    return BadRequest("Relação já feita anteriormente");
                }
                return BadRequest("Role inválida");
            }
            return BadRequest("Usuário inválido");
        }

        ///// <summary>
        ///// Editar relação user e role.
        ///// </summary>
        //[HttpPut]
        //public async Task<IActionResult> PutUserRole(int userId, int roleId)
        //{
        //    var user = await _context.Users.Where(u => u.Id == userId).ToListAsync();
        //    if (user != null)
        //    {
        //        var role = await _context.Roles.Where(r => r.Id == roleId).ToListAsync();
        //        if (role != null)
        //        {
        //            var usersroles = _context.UsersRoles.Find(userId, roleId);
        //            if (usersroles != null)
        //            {
        //                try
        //                {
        //                    var usersrolesAtt = new UserRole()
        //                    {
        //                        //Id = usersroles.Id,
        //                        UserId = userId != 0 ? userId : usersroles.UserId,
        //                        RoleId = roleId != 0 ? roleId : usersroles.RoleId,
        //                    };

        //                    if (usersrolesAtt != null)
        //                    {
        //                        _context.UsersRoles.Update(usersrolesAtt);
        //                        _context.SaveChanges();
        //                        return Ok("Role editada com sucesso!");
        //                    }
        //                }
        //                catch (Exception)
        //                {

        //                    throw;
        //                }
        //            }
        //            return BadRequest("Relação inválida");
        //        }
        //        return BadRequest("Role inválida");
        //    }
        //    return BadRequest("Usuário inválido");
        //}

        /// <summary>
        /// Deletar relação.
        /// </summary>
        [HttpDelete]
        //[Authorize(Roles = "manager")]
        public ActionResult Delete(int userid, int roleid)
        {
            try
            {
                var usersroles = _context.UsersRoles.Where(u => u.UserId == userid && u.RoleId == roleid).Single();
                if (usersroles != null)
                {
                    _context.UsersRoles.Remove(usersroles);
                    _context.SaveChanges();
                    return Ok("Deletado com sucesso!");
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
