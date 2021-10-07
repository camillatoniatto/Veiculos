using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veiculo.Dominio;

namespace Veiculo.Repositorio
{
    public class VeiculoContext : DbContext
    {
        public VeiculoContext(DbContextOptions<VeiculoContext> options) : base(options) { } //manda options para statup.cs

        public DbSet<Carro> Carros { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
    }
}
