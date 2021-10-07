using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Veiculo.Dominio;

namespace Veiculo.Repositorio
{
    public interface IVeiculoRepositorio
    {
        //INTERFACE!!
        //metodos genericos aceitando qualquer variavel com tipo qualquer
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Task<bool> SaveChangeAsync();


        Task<Carro[]> GetAllCarros(); //retornar uma listagem de carros
        Task<Carro> GetCarroById(int id); //retorna apenas um carro pelo id
        ////Task<Carro[]> GetCarrosByEstado(string estado); //retorna carros pelo estado
    }
}
