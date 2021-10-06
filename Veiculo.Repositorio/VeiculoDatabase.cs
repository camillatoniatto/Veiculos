using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veiculo.Dominio;

namespace Veiculo.Repositorio
{
    public class VeiculoDatabase : DbContext
    {
        public VeiculoDatabase(DbContextOptions<VeiculoDatabase> options) : base(options) { }
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
    }
}
