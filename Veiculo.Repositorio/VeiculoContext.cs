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
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UsersRoles { get; set; }
        public DbSet<Agendador> Agendadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>(entity => //CHAVE COMPOSTA
            {
                entity.HasKey(e => new { e.UserId, e.RoleId }); //a chave é composta de e.UserId e e.RoleId
            });
        }
    }
}
