﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veiculo.Dominio;
using Veiculo.Repositorio;
using Veiculos.ViewModels;

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
        //[Authorize(Roles = "manager,employee,support,client")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var reservas = await _context.Reservas.ToListAsync();
                var list = new List<ReservasViewModel>();
                foreach (var reserva in reservas)
                {
                    list.Add(new ReservasViewModel(reserva.Id.ToString(), reserva.DtInicio.ToString("g"), reserva.DtFim.ToString("g"), reserva.CarroId.ToString()));
                }
                return Ok(list);
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
        //[Authorize(Roles = "manager,employee,support")]
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
        //[Authorize(Roles = "manager,employee,support")]
        public async Task<IActionResult> Post(Reserva model)
        {
            if (model.DtInicio < model.DtFim)
            {
                //verifica de o carro existe
                var carro = await _context.Carros.AsNoTracking().FirstOrDefaultAsync(c => c.Id == model.CarroId);
                if (carro != null)
                {
                    //var listaReservas = await _context.Reservas.Where(d => d.DtInicio >= model.DtInicio && d.DtFim <= model.DtFim).ToListAsync(); //verifica os veiculos reservados em x data
                    //var carrosReservados = listaReservas.Select(r => r.CarroId); //pega carroId dos veiculos reservados em x data
                    //var carrosAvailable = _context.Carros.Where(c => !carrosReservados.Contains(c.Id) && c.Estado == "available").ToList(); //compara carroId com Id dos veiculos restantes (IsNotIn)
                    //if (carrosAvailable.Contains(model.CarroId))

                    var listaReservas = await _context.Reservas.Where(d => d.DtInicio >= model.DtInicio && d.DtFim <= model.DtFim && d.CarroId == model.CarroId).Select(d => d.CarroId).ToListAsync();
                    var carrosAvailable = _context.Carros.Where(c => listaReservas.Contains(c.Id) && c.Estado == "available").ToList();
                    if (carrosAvailable.Count() == 0)
                    {
                        try
                        {
                            var newReserva = new Reserva()
                            {
                                DtInicio = model.DtInicio,
                                DtFim = model.DtFim,
                                CarroId = model.CarroId
                            };
                            _context.Reservas.Add(newReserva);
                            _context.SaveChanges();
                            return Ok("Reserva feita com sucesso!");
                        }
                        catch (Exception ex)
                        {
                            return BadRequest($"Erro: {ex}");
                        }
                    }
                    return Ok("Este veículo está reservado \nPor favor verifique os veículos disponíveis em 'GetAvailableCars'");
                }
                return Ok("O veículo não existe");                
            }
            return Ok("Data inválida");
        }

        // PUT api/<ReservaController>/5
        /// <summary>
        /// Alterar reserva.
        /// </summary> 
        [HttpPut("{id}")]
        //[Authorize(Roles = "manager,employee")]
        public async Task<IActionResult> Put(int id, Reserva model)
        {
            if (model.DtInicio < model.DtFim)
            {
                var carro = await _context.Carros.AsNoTracking().FirstOrDefaultAsync(c => c.Id == model.CarroId);
                if (carro != null)
                {
                    var listaReservas = await _context.Reservas.Where(d => d.DtInicio >= model.DtInicio && d.DtFim <= model.DtFim && d.CarroId == model.CarroId).Select(d => d.CarroId).ToListAsync();
                    var carrosAvailable = _context.Carros.Where(c => listaReservas.Contains(c.Id) && c.Estado == "available").ToList();
                    if (carrosAvailable.Count() == 0)
                    {
                        try
                        {
                            var reserva = await _context.Reservas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                            var reservaAtt = new Reserva()
                            {
                                Id = reserva.Id,
                                DtInicio = model.DtInicio != null ? model.DtInicio : reserva.DtInicio,
                                DtFim = model.DtFim != null ? model.DtFim : reserva.DtFim,
                                CarroId = model.CarroId != 0 ? model.CarroId : reserva.CarroId,
                            };

                            _context.Reservas.Update(reservaAtt);
                            _context.SaveChanges();
                            return Ok("Editado com sucesso!");
                        }
                        catch (Exception ex)
                        {
                            return BadRequest($"Erro: {ex}");
                        }
                    }
                    return Ok("Este veículo está reservado \nPor favor verifique os veículos disponíveis em 'GetAvailableCars'");
                }
                return Ok("O veículo não existe");
            } 
            return Ok("Data inválida");
        }

        // DELETE api/<ReservaController>/5
        /// <summary>
        /// Deletar reserva.
        /// </summary>
        [HttpDelete("{id}")]
        //[Authorize(Roles = "manager")]
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