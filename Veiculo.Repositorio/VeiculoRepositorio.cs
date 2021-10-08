using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veiculo.Dominio;

namespace Veiculo.Repositorio
{
    public class VeiculoRepositorio : IVeiculoRepositorio
    {
        private readonly VeiculoContext _context;

        //CLASSE QUE IMPLEMENTA A INTERFACE!!
        public VeiculoRepositorio(VeiculoContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangeAsync()
        {
            //espera até que o savechanges tenha ocorrido e analisa se é > 0
            return (await _context.SaveChangesAsync()) > 0;
        }



        public async Task<Carro[]> GetAllCarros()
        {
            IQueryable<Carro> query = _context.Carros; //montando a query para fazer a execução

            query = query.AsNoTracking().OrderBy(h => h.Id); //consulta utilizando asnotracking e ordenando pelo id

            return await query.ToArrayAsync();
        }

        public async Task<Carro> GetCarroById(int id)
        {
            IQueryable<Carro> query = _context.Carros;

            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.FirstOrDefaultAsync(h => h.Id == id);
        }
    }
}
